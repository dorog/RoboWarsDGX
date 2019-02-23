using UnityEngine;
using Photon.Pun;

public class CharacterInit : MonoBehaviourPun
{

    public GameObject[] fullBody;
    public GameObject[] firsPerson;

    void Awake()
    {
        if (photonView.IsMine)
        {
            /*foreach (GameObject bodyPart in fullBody)
            {
                bodyPart.SetActive(false);
            }*/
            foreach (GameObject bodyPart in firsPerson)
            {
                bodyPart.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject bodyPart in fullBody)
            {
                bodyPart.SetActive(true);
            }
            foreach (GameObject bodyPart in firsPerson)
            {
                bodyPart.SetActive(false);
            }
        }
    }
}
