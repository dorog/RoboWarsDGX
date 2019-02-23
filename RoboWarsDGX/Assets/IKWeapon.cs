using UnityEngine;

public class IKWeapon : MonoBehaviour
{

    private Animator anim;
    public GameObject weapon;
    private Transform GunHold;

    public float amount = 0.5f;

    void Start()
    {
        anim = GetComponent<Animator>();

        Transform rightHand = anim.GetBoneTransform(HumanBodyBones.RightHand);
        GameObject weaponGO = Instantiate(weapon, rightHand);
        GunHold = weaponGO.transform.GetChild(0);

    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, amount);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, GunHold.position);
    }
}
