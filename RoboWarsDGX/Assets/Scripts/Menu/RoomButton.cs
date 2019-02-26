using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    public Text roomName;
    public Text roomSize;
    public Image roomButton;

    private RoomInfo info;
    private JoinRoomUI joinRoomUI;

    public void Init(RoomInfo roomInfo, JoinRoomUI joinRoomUI)
    {
        roomName.text = roomInfo.Name;
        roomSize.text = "(" + roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers + ")";
        info = roomInfo;
        this.joinRoomUI = joinRoomUI;
    }

    public void ShowRoom()
    {
        joinRoomUI.SelectRoom(info, roomButton);
    }
}
