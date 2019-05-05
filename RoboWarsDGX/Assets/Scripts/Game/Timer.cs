using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviourPun
{
    private float timeLeft = Mathf.Infinity;
    public Text counter;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Room currentRoom = PhotonNetwork.CurrentRoom;
            timeLeft = (float)currentRoom.CustomProperties[SharedData.TimeKey];
        }
        else
        {
            //Ask the time
            Debug.Log("Ask time");
            photonView.RPC("GetRemainTime", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    private void GetRemainTime()
    {
        Debug.Log("Arrived");
        photonView.RPC("RefreshTime", RpcTarget.Others, timeLeft);
    }

    [PunRPC]
    private void RefreshTime(float time)
    {
        timeLeft = time;
    }

    private void Update()
    {
        if (timeLeft != Mathf.Infinity)
        {
            timeLeft -= Time.deltaTime;
            counter.text = GetTime();
        }
    }

    private string GetTime()
    {
        int time = (int)timeLeft;
        float seconds = time % 60;
        float minutes = time / 60;
        if(seconds < 10)
        {
            return minutes + ":0" + seconds;
        }
        else
        {
            return minutes + ":" + seconds;
        }
    }
}
