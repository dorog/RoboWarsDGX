using UnityEngine;
using Photon.Pun;

public class DeathMatchManager : MonoBehaviourPun, IPunObservable
{

    public GameObject[] spawnPoints;

    public void SpawnPlayer()
    {
        PhotonNetwork.Instantiate("Characters/" + SelectData.selectedCharacter.id, spawnPoints[GetSpawnPoint()].transform.position, Quaternion.identity, 0);
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
}
