using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public static PhotonLobby Instance = null;
    public List<RoomInfo> rooms = null;

    public JoinRoomUI joinRoomUI;

    private string roomMap = SharedData.desertMap;
    private string roomGameMode = SharedData.deathMatch;

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

    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Settings");
            PhotonNetwork.ConnectUsingSettings();

        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");
        rooms = roomList;
        joinRoomUI.Refresh();
    }

    public void CreateRoom(string roomName, byte maxPlayer, string map, string gameMode, int spawnMode)
    {
        Debug.Log("CreateRoom");
        //this.map = map;
        roomMap = map;
        roomGameMode = gameMode;

        PhotonNetwork.AutomaticallySyncScene = true;

        ExitGames.Client.Photon.Hashtable hastable = new ExitGames.Client.Photon.Hashtable() { { SharedData.MapKey, roomMap }, { SharedData.GameModeKey, roomGameMode }, { SharedData.SpawnModeKey, spawnMode} };
        string[] customProperties = new string[] { SharedData.MapKey, SharedData.GameModeKey, SharedData.SpawnModeKey};

        RoomOptions ro = new RoomOptions { MaxPlayers = maxPlayer, IsOpen = true, IsVisible = true, CustomRoomPropertiesForLobby = customProperties, CustomRoomProperties = hastable };
        PhotonNetwork.CreateRoom(roomName, ro, TypedLobby.Default);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed");
        //Same name
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        /*Room currentRoom = PhotonNetwork.CurrentRoom;
        
        ExitGames.Client.Photon.Hashtable hastable = new ExitGames.Client.Photon.Hashtable() { { SharedData.MapKey, roomMap } };
        currentRoom.SetCustomProperties(hastable);*/
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        base.OnJoinedRoom();
        SceneManager.LoadScene("Desert");
    }

    public void RefreshRooms()
    {
        Debug.Log("RefreshRooms");
        if (!PhotonNetwork.InLobby)
        {
            Debug.Log("InLobby");
            PhotonNetwork.JoinLobby();
        }
    }

}
