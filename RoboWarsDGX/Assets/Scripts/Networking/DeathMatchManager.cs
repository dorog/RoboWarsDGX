using UnityEngine;
using Photon.Pun;

public class DeathMatchManager : MonoBehaviourPun, IPunObservable
{

    public GameObject[] spawnPoints;
    //public GameObject cube;

    private void Start()
    {

    }

    public void SpawnPlayer()
    {
        GameObject character = PhotonNetwork.Instantiate("Characters/" + SelectData.selectedCharacter.id + "/" + SelectData.selectedCharacter.id + "With" + SelectData.selectedWeapon.id, spawnPoints[GetSpawnPoint()].transform.position, Quaternion.identity, 0);
        /*int characterID = character.GetComponent<PhotonView>().ViewID;

        GameObject weapon = PhotonNetwork.Instantiate("Weapons/" + SelectData.selectedWeapon.id, Vector3.zero, Quaternion.identity, 0);
        int weaponID = weapon.GetComponent<PhotonView>().ViewID;

        Vector3 position = SelectData.selectedWeapon.prefab.transform.position;
        Quaternion rotation = SelectData.selectedWeapon.prefab.transform.rotation;

        photonView.RPC("SetWeapon", RpcTarget.All, characterID, weaponID, position, rotation);*/
        /*photonView.RPC("dfsa", RpcTarget.)*/
    }

    private int GetSpawnPoint()
    {
        return 0;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

        }
        else if (stream.IsReading)
        {

        }
    }

    /*[PunRPC]
    void SetWeapon(int characterID, int weaponID, Vector3 position, Quaternion rotation)
    {
        Debug.Log("Called");
        Debug.Log(characterID + " " + weaponID);
        GameObject character = PhotonView.Find(characterID).gameObject;
        GameObject weapon = PhotonView.Find(weaponID).gameObject;

        IKWeapon ikWeapon = character.GetComponentInChildren<IKWeapon>();
        weapon.transform.SetParent(ikWeapon.rightHand);
        weapon.transform.localPosition = position;
        weapon.transform.localRotation = rotation;
        ikWeapon.GunHold = weapon.transform.GetChild(0);
    }*/
}
