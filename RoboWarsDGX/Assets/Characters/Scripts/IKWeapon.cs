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

        /*Transform rightHand = anim.GetBoneTransform(HumanBodyBones.RightHand);*/
        /*if(rightHand == null)
        {
            Debug.Log("null");
        }*/

        /*if (photonView.IsMine)
        {
            GameObject weaponGO = PhotonNetwork.Instantiate("Weapons/" + SelectData.selectedWeapon.id, Vector3.zero, Quaternion.identity, 0);
            Vector3 position = SelectData.selectedWeapon.prefab.transform.position;
            Quaternion rotation = SelectData.selectedWeapon.prefab.transform.rotation;
            photonView.RPC("SetWeapon", RpcTarget.All, weaponGO.GetComponent<PhotonView>().ViewID, GetComponent<PhotonView>().ViewID, position, rotation);
        }*/
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(GunHold != null)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, amount);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, GunHold.position);
        }
    }

    /*[PunRPC]
    void SetWeapon(int weaponID, int characterID, Vector3 position, Quaternion rotation)
    {
        GameObject weapon = PhotonView.Find(id).gameObject;
        weapon.transform.SetParent(rightHand);
        weapon.transform.localPosition = position;
        weapon.transform.localRotation = rotation;
        GunHold = weapon.transform.GetChild(0);
    }*/
}
