using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GameModeManager: MonoBehaviourPun
{
    public GameObject[] spawnPoints;
    public WeaponInitData alex;
    public WeaponInitData amy;

    private IGameMode gameModeSpawner;

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
    }

    public void SpawnPlayer() {
        SpawnedPlayerData spawnedPlayerData = gameModeSpawner.SpawnPlayer(spawnPoints[0].transform.position);
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
        GameObject character = PhotonView.Find(characterID).gameObject;
        GameObject weapon = PhotonView.Find(weaponID).gameObject;
        if (character == null || weapon == null)
        {
            return;
        }

        WeaponInitData weaponInitData = GetWeaponInitData(characterName);

        IKWeapon ikWeapon = character.GetComponentInChildren<IKWeapon>();
        weapon.transform.SetParent(ikWeapon.rightHand);
        weapon.transform.localPosition = weaponInitData.GetWeaponPosition(weaponName);
        weapon.transform.localRotation = Quaternion.Euler(weaponInitData.GetWeaponRotation(weaponName));
        weapon.transform.GetChild(0).localPosition = weaponInitData.GetGunHoldPosition(weaponName);

        ikWeapon.GunHold = weapon.transform.GetChild(0);
    }
}
