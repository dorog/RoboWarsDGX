using UnityEngine;
using Photon.Pun;
using Photon;

public class CharacterFiring : MonoBehaviourPun
{
    private Animator animator;

    private Transform spine;
    private Transform spine1;
    private Transform spine2;

    private Vector3 aim = Vector3.zero;

    public float multiply = 1f;
    public float maxRotation = 10f;
    public float minRotation = -10f;

    void Start()
    {
        animator = GetComponent<Animator>();

        spine = animator.GetBoneTransform(HumanBodyBones.Spine);
        spine1 = animator.GetBoneTransform(HumanBodyBones.Chest);
        spine2 = animator.GetBoneTransform(HumanBodyBones.UpperChest);


        Cursor.lockState = CursorLockMode.Locked;
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

    private void LateUpdate()
    {
        if (photonView.IsMine)
        {
            float rotationY = Input.GetAxis("Mouse Y");

            if (aim.x - rotationY * multiply > maxRotation)
            {
                aim.x = maxRotation;
            }
            else if (aim.x - rotationY * multiply < minRotation)
            {
                aim.x = minRotation;
            }
            else
            {
                aim.x -= rotationY * multiply;
            }


            spine.Rotate(aim);
            spine1.Rotate(aim);
            spine2.Rotate(aim);
        }
    }
}
