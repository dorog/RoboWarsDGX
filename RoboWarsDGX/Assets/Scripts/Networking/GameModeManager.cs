using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GameModeManager: MonoBehaviourPun
{
    public static GameModeManager Instance = null;

    public GameObject[] spawnPoints;
    public WeaponInitData alex;
    public WeaponInitData amy;

    public PlaySelect lobby;

    private IGameMode gameModeSpawner;
    private SpawnPointSearcher spawPointChooser;

    public Transform characterParent;
    public float[] ranges;

    private delegate Vector3 SpawnPointGetter();
    private event SpawnPointGetter GetSpawnPoint;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Room currentRoom = PhotonNetwork.CurrentRoom;
        GameMode gameMode = SharedData.GetGameMode((string)currentRoom.CustomProperties[SharedData.GameModeKey]);

        spawPointChooser = new SpawnPointSearcher(spawnPoints, characterParent);
        SpawnMode spawnMode = (SpawnMode)currentRoom.CustomProperties[SharedData.SpawnModeKey];

        switch (gameMode)
        {
            case GameMode.DeathMatch:
                gameModeSpawner = new DeathMatchMode();
                break;
            case GameMode.TeamDeathMatch:
                gameModeSpawner = new TeamDeathMatchMode();
                break;
            default:
                break;
        }
        switch (spawnMode)
        {
            case SpawnMode.DistanceBased:
                GetSpawnPoint += spawPointChooser.DistanceBasedSpawnPoint;
                break;
            case SpawnMode.AreaBased:
                GetSpawnPoint += spawPointChooser.AreaBasedSpawnPoint;
                break;
            default:
                GetSpawnPoint += spawPointChooser.RandomSpawnPoint;
                break;
        }
    }

    public void SpawnPlayer() {
        SpawnedPlayerData spawnedPlayerData = gameModeSpawner.SpawnPlayer(spawPointChooser.DistanceBasedSpawnPoint());
        photonView.RPC("SetHierarchy", RpcTarget.AllBuffered, spawnedPlayerData.characterID, spawnedPlayerData.weaponID, spawnedPlayerData.characterName, spawnedPlayerData.weaponName);
    }

    private WeaponInitData GetWeaponInitData(string characterName)
    {
        switch (characterName)
        {
            case "Alex":
                return alex;
            default:
                return amy;
        }
    }

    [PunRPC]
    private void SetHierarchy(int characterID, int weaponID, string characterName, string weaponName)
    {
        PhotonView characterView = PhotonView.Find(characterID);
        if (characterView == null)
        {
            return;
        }
        PhotonView weaponView = PhotonView.Find(weaponID);
        if(weaponView== null) {
            return;
        }

        GameObject character = characterView.gameObject;
        character.transform.SetParent(characterParent);
        GameObject weapon = weaponView.gameObject;

        WeaponInitData weaponInitData = GetWeaponInitData(characterName);

        IKWeapon ikWeapon = character.GetComponentInChildren<IKWeapon>();
        weapon.transform.SetParent(ikWeapon.rightHand);
        weapon.transform.localPosition = weaponInitData.GetWeaponPosition(weaponName);
        weapon.transform.localRotation = Quaternion.Euler(weaponInitData.GetWeaponRotation(weaponName));

        WeaponPrefab weaponPrefab = weapon.GetComponent<WeaponPrefab>();
        weaponPrefab.GunHoldTransform.localPosition = weaponInitData.GetGunHoldPosition(weaponName);

        CharacterFiring characterFiring = character.GetComponent<CharacterFiring>();
        characterFiring.thirdPersonWeapon = weapon.GetComponent<FiringWeapon>();

        ikWeapon.GunHold = weapon.transform.GetChild(0);
    }

    public void Died()
    {
        lobby.Died();
    }
}
