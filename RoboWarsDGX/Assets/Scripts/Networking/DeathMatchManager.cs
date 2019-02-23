﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class DeathMatchManager : MonoBehaviourPun, IPunObservable
{
    public float SpawnTime;
    private float timer = 0;
    private bool HasPlayerSpawned = false;

    public GameObject[] spawnPoints;

    private int spawnCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= SpawnTime)
        {
            timer = 0;
            if (!HasPlayerSpawned)
            {
                PhotonNetwork.Instantiate("Amy", spawnPoints[spawnCount].transform.position, Quaternion.identity, 0);
                spawnCount++;
                HasPlayerSpawned = true;
            }
        }
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
