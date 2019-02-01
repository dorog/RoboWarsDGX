using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class MPManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {

    }

    public void JoinDeathMatch()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        JoinDeathMatch();
    }

    public void CreateDeathMatch()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        RoomOptions ro = new RoomOptions { MaxPlayers = 8, IsOpen = true, IsVisible = true };
        PhotonNetwork.CreateRoom("DeathMatch", ro, TypedLobby.Default);

        SceneManager.LoadScene("DeathMatch");
    }
}
