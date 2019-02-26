using UnityEngine;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;

public class JoinRoomUI : MonoBehaviour
{
    public GameObject noRoomText;
    public Transform parent;
    public RoomButton roomButton;
    public Color selectedButtonColor;

    [Header ("Selected Room Settings")]
    public Image mapImage;
    public Text mapName;
    public Text gameMode;
    public GameObject joinButton;
    

    private RoomInfo selectedRoomInfo = null;
    private Image selectedButton = null;
    private Color originalColor;

    private void Start()
    {
        joinButton.SetActive(false);
    }

    private void RoomsUpdate(List<RoomInfo> rooms)
    {
        RemoveRooms();

        for (int i=0; i<rooms.Count; i++)
        {
            CreateRoom(rooms[i]);
        }
    }

    private void RemoveRooms()
    {
        for (int i = parent.childCount - 1; i >= 0; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

    private void CreateRoom(RoomInfo actualRoom)
    {
        if (actualRoom.IsOpen && actualRoom.IsVisible)
        {
            GameObject tempListing = Instantiate(roomButton.gameObject, parent);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.Init(actualRoom, this);
        }
    }

    public void Refresh()
    {
        List<RoomInfo> availabeRooms = PhotonLobby.Instance.rooms;
        if (availabeRooms == null || availabeRooms.Count == 0)
        {
            noRoomText.SetActive(true);
            RemoveRooms();
        }
        else
        {
            noRoomText.SetActive(false);
            RoomsUpdate(availabeRooms);
        }
    }

    public void SelectRoom(RoomInfo room, Image roomButton)
    {
        joinButton.SetActive(true);
        selectedRoomInfo = room;
        if(selectedButton != null)
        {
            selectedButton.color = originalColor;
        }

        originalColor = roomButton.color;
        roomButton.color = selectedButtonColor;    

        mapName.text = (string)room.CustomProperties[SharedData.MapKey];
        gameMode.text  = (string)room.CustomProperties[SharedData.GameModeKey];


        Debug.Log("Other settings...");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(selectedRoomInfo.Name);
    }
}
