using UnityEngine;

public class IKWeapon : MonoBehaviour
{

    private Animator anim;
    private Transform GunHold;

    public float amount = 0.5f;

    void Start()
    {
        anim = GetComponent<Animator>();

        Transform rightHand = anim.GetBoneTransform(HumanBodyBones.RightHand);
        GameObject weaponGO = Instantiate(SelectData.selectedWeapon.prefab, rightHand);
        GunHold = weaponGO.transform.GetChild(0);

    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, amount);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, GunHold.position);
    }
}
