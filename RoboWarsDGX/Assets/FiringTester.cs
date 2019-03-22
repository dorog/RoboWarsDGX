using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringTester : MonoBehaviour
{
    public Animator anim;
    public string fireCommand = "SniperFire";
    public bool fire = false;
    public string shotgun = "Shotgun";

    private void Start()
    {
        anim.SetBool(shotgun, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (fire)
        {
            fire = false;
            anim.SetBool(fireCommand, true);
            Invoke("Stop", 0.1f);
        }
    }

    private void Stop()
    {
        anim.SetBool(fireCommand, false);
    }
}
