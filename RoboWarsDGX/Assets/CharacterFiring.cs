using UnityEngine;
using Photon.Pun;
using Photon;

public class CharacterFiring : MonoBehaviourPun
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("Firing", true);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                animator.SetBool("Firing", false);
            }
        }
    }
}
