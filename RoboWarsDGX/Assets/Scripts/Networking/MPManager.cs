
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class MPManager : MonoBehaviourPunCallbacks
{
    public GameObject[] EnableObjectsOnConnect;

    private string map;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        foreach(GameObject obj in EnableObjectsOnConnect)
        {
            obj.SetActive(true);
        }
    }

    public void JoinDeathMatch()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateDeathMatch();
    }

    public void CreateDeathMatch()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        RoomOptions ro = new RoomOptions { MaxPlayers = 8, IsOpen = true, IsVisible = true };
        PhotonNetwork.CreateRoom("Desert", ro, TypedLobby.Default);
    }

    public bool GetConnectionState()
    {
        return PhotonNetwork.IsConnected;
    }

    public void CreateGame(string roomName, byte maxPlayer, string map)
    {

        this.map = map;

        PhotonNetwork.AutomaticallySyncScene = true;

        RoomOptions ro = new RoomOptions { MaxPlayers = maxPlayer, IsOpen = true, IsVisible = true };
        PhotonNetwork.CreateRoom(roomName, ro, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        //TODO: loadAsync and animation
        SceneManager.LoadScene(map);
    }

    /*public void GetRooms()
    {
        PhotonNetwork.GetCustomRoomList(TypedLobby.Default, "");
    }*/

    void OnReceivedRoomListUpdate()
    {
        //RoomInfo[] rooms = PhotonNetwork.GetRoomList();
    }

}
