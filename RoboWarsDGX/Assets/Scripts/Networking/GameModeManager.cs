using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GameModeManager: MonoBehaviourPun
{
    public static GameModeManager Instance = null;
    public WeaponBullet bullet;
    public InstantBullet instantBullet;
    public Transform bulletParent;

    public GameObject[] spawnPoints;
    public WeaponInitData alex;
    public WeaponInitData amy;

    public GameObject lobbyForRespawn;

    private IGameMode gameModeSpawner;

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
        switch (gameMode)
        {
            case GameMode.DeathMatch:
                gameModeSpawner = new DeathMatchMode();
                break;
            default:
                break;
        }

        string displayName = AccountInfo.Instance.Info.PlayerProfile.DisplayName;
        photonView.RPC("NewPlayer", RpcTarget.AllBuffered, displayName);
    }

    [PunRPC]
    private void NewPlayer(string displayName)
    {

    }

    public void SpawnPlayer() {
        SpawnedPlayerData spawnedPlayerData = gameModeSpawner.SpawnPlayer(spawnPoints[0].transform.position);
        lobbyForRespawn.SetActive(false);
        photonView.RPC("AddWeapon", RpcTarget.AllBuffered, spawnedPlayerData.characterID, spawnedPlayerData.weaponID, spawnedPlayerData.characterName, spawnedPlayerData.weaponName);
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
    private void AddWeapon(int characterID, int weaponID, string characterName, string weaponName)
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
        GameObject weapon = weaponView.gameObject;

        if (characterView.IsMine && weaponView.IsMine)
        {
            weapon.layer = LayerMask.NameToLayer(SharedData.NotRenderInIsMine);
        }

        WeaponInitData weaponInitData = GetWeaponInitData(characterName);

        IKWeapon ikWeapon = character.GetComponentInChildren<IKWeapon>();
        weapon.transform.SetParent(ikWeapon.rightHand);
        weapon.transform.localPosition = weaponInitData.GetWeaponPosition(weaponName);
        weapon.transform.localRotation = Quaternion.Euler(weaponInitData.GetWeaponRotation(weaponName));

        WeaponPrefab weaponPrefab = weapon.GetComponent<WeaponPrefab>();
        weaponPrefab.GunHoldTransform.localPosition = weaponInitData.GetGunHoldPosition(weaponName);

        ikWeapon.GunHold = weapon.transform.GetChild(0);
    }

    public void SpawnBullet(Vector3 position, Vector3 forward)
    {
        photonView.RPC("SpawnBulletRPC", RpcTarget.All, position, forward);
    }

    [PunRPC]
    private void SpawnBulletRPC(Vector3 position, Vector3 forward, int viewId)
    {
        GameObject bulletGO = Instantiate(bullet.gameObject, position, Quaternion.identity, bulletParent);
        Rigidbody bulletRigidbody = bulletGO.GetComponent<Rigidbody>();
        bulletRigidbody.AddRelativeForce(forward * 715, ForceMode.Impulse);
    }

    public void SpawnInstantBulletRayCast(Vector3 position, Vector3 forward, float dmg, float distance, string id)
    {
        photonView.RPC("SpawnInstantBulletRPC", RpcTarget.All, position, forward, dmg, distance, id);
    }

    [PunRPC]
    private void SpawnInstantBulletRPC(Vector3 position, Vector3 forward, float dmg, float distance, string id)
    {
        GameObject instantBulletGO = Instantiate(instantBullet.gameObject, position, Quaternion.identity, bulletParent);
        instantBulletGO.transform.forward = forward;
        InstantBullet instantBulletScript = instantBulletGO.GetComponent<InstantBullet>();
        instantBulletScript.dmg = dmg;
        instantBulletScript.maxDistance = distance;
        instantBulletScript.playerId = id;
        instantBulletScript.Fire();
    }

    public void SpawnInstantBulletCapsuleCast()
    {
        //TODO: Shotgun
    }

    public void ShowLobby()
    {
        lobbyForRespawn.SetActive(true);
        /*photonView.RPC("DestroyPlayer", RpcTarget.All, characterID);*/
    }
}
