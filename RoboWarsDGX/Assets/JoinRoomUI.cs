using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class JoinRoomUI : MonoBehaviour
{
    public Text noRoomText;
    public Transform parent;

    private int roomCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void GetRooms()
    {
        /*PhotonNetwork.
        RoomInfo[] roominfos = manager.GetRooms();*/
    }
}
