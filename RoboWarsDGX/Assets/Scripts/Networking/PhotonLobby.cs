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
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        rooms = roomList;
        joinRoomUI.Refresh();
    }

    public void CreateRoom(string roomName, byte maxPlayer, string map, string gameMode, int spawnMode)
    {
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
        //Same name
    }

    public override void OnCreatedRoom()
    {
        /*Room currentRoom = PhotonNetwork.CurrentRoom;
        
        ExitGames.Client.Photon.Hashtable hastable = new ExitGames.Client.Photon.Hashtable() { { SharedData.MapKey, roomMap } };
        currentRoom.SetCustomProperties(hastable);*/
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        SceneManager.LoadScene("Desert");
    }

    public void RefreshRooms()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

}
