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

    [Header("Metal settings")]
    public PhysicMaterial metalMat;
    public GameObject metalHit;

    [Header("Wood settings")]
    public PhysicMaterial woodMat;
    public GameObject woodHit;

    [Header("Stone settings")]
    public PhysicMaterial stoneMat;
    public GameObject stoneHit;

    [Header("Sand settings")]
    public PhysicMaterial sandMat;
    public GameObject sandHit;

    [Header("Filled container settings")]
    public PhysicMaterial filledContainerMat;
    public GameObject filledContainerHit;

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
        photonView.RPC("SetHierarchy", RpcTarget.AllBuffered, spawnedPlayerData.characterID, spawnedPlayerData.weaponID, spawnedPlayerData.characterName, spawnedPlayerData.weaponName, (int)SelectData.selectedWeapon.type);
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
    private void SetHierarchy(int characterID, int weaponID, string characterName, string weaponName, int weaponType)
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

        
        characterFiring.SetSoundSettings((WeaponType)weaponType);

        ikWeapon.GunHold = weapon.transform.GetChild(0);
    }

    public void Died()
    {
        lobby.Died();
    }

    public void SpawnMapDamage(PhysicMaterial mat, Vector3 position, Vector3 normal)
    {
        Debug.Log("Called spawn dmg");
        if(mat == null)
        {
            Debug.Log("No mat!");
            return;
        }

        MaterialType matType = GetMatType(mat);

        if (matType != MaterialType.Null)
        {
            photonView.RPC("SpawnMapDamageRPC", RpcTarget.All, (int)matType, position.x, position.y, position.z, normal.x, normal.y, normal.z);
        }
    }

    [PunRPC]
    private void SpawnMapDamageRPC(int mat, float positionX, float positionY, float positionZ, float normalX, float normalY, float normalZ)
    {
        Vector3 position = new Vector3(positionX, positionY, positionZ);
        Vector3 normal = new Vector3(normalX, normalY, normalZ);

        MaterialType matType = (MaterialType)mat;

        switch (matType)
        {
            case MaterialType.metal:
                Instantiate(metalHit, position, Quaternion.LookRotation(normal), transform);
                break;
            case MaterialType.wood:
                Instantiate(woodHit, position, Quaternion.LookRotation(normal), transform);
                break;
            case MaterialType.stone:
                Instantiate(stoneHit, position, Quaternion.LookRotation(normal), transform);
                break;
            case MaterialType.sand:
                Instantiate(sandHit, position, Quaternion.LookRotation(normal), transform);
                break;
            case MaterialType.waterContainer:
                Instantiate(filledContainerHit, position, Quaternion.LookRotation(normal), transform);
                break;
            default:
                break;
        }
    }

    private MaterialType GetMatType(PhysicMaterial mat)
    {
        if (metalMat.Equals(mat))
        {
            return MaterialType.metal;
        }
        else if (woodMat.Equals(mat))
        {
            return MaterialType.wood;
        }
        else if (stoneMat.Equals(mat))
        {
            return MaterialType.stone;
        }
        else if (sandMat.Equals(mat))
        {
            return MaterialType.sand;
        }
        else if (filledContainerMat.Equals(mat))
        {
            return MaterialType.waterContainer;
        }
        else
        {
            return MaterialType.Null;
        }
    }
}
