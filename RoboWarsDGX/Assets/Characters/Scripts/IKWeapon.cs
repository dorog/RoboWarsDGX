using UnityEngine;
using Photon.Pun;

public class IKWeapon : MonoBehaviourPun
{

    private Animator anim;
    public Transform GunHold;
    public Transform rightHand;

    public float amount = 0.5f;

    void Start()
    {
        anim = GetComponent<Animator>();

    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(GunHold != null)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, amount);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, GunHold.position);
        }
    }
}
