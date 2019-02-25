using UnityEngine;
using Photon.Pun;

public class DeathMatchManager : MonoBehaviourPun
{
    public GameObject[] spawnPoints;

    public WeaponInitData alex;
    public WeaponInitData amy;

    public void SpawnPlayer()
    {
        GameObject character = PhotonNetwork.Instantiate("Characters/" + SelectData.selectedCharacter.id, spawnPoints[GetSpawnPoint()].transform.position, Quaternion.identity, 0);

        int characterID = character.GetComponent<PhotonView>().ViewID;

        GameObject weapon = PhotonNetwork.Instantiate("Weapons/" + SelectData.selectedWeapon.id, Vector3.zero, Quaternion.identity, 0);
        int weaponID = weapon.GetComponent<PhotonView>().ViewID;

        Vector3 position = SelectData.selectedWeapon.prefab.transform.position;
        Quaternion rotation = SelectData.selectedWeapon.prefab.transform.rotation;

        photonView.RPC("AddWeapon", RpcTarget.AllBuffered, characterID, weaponID, SelectData.selectedCharacter.id, SelectData.selectedWeapon.id);

    }

    private int GetSpawnPoint()
    {
        return 0;
    }

    [PunRPC]
    void AddWeapon(int characterID, int weaponID, string characterName, string weaponName)
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
}
