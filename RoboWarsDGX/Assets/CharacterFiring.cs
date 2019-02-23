using UnityEngine;
using Photon.Pun;

public class CharacterFiring : MonoBehaviourPun, IPunObservable
{
    public Animator firstPerson;
    public Animator thirdPerson;

    [Header("First person spines")]
    public Transform firstPersonSpine;
    public Transform firstPersonSpine1;
    public Transform firstPersonSpine2;

    private Transform thirdPersonSpine;
    private Transform thirdPersonSpine1;
    private Transform thirdPersonSpine2;

    private float thirdPersonAimX = 0;
    private float firstPersonAimX = 0;

    private float aimY = 0;

    [Header ("Look up settings")]
    public float multiply = 1f;
    public float maxRotation = 10f;
    public float minRotation = -10f;
    public float firstPersonLookMultiply = 3f;

    private float cloneX = 0f;

    void Start()
    {

        /*firstPersonSpine = firstPerson.GetBoneTransform(HumanBodyBones.Spine);
        firstPersonSpine1 = firstPerson.GetBoneTransform(HumanBodyBones.Chest);
        firstPersonSpine2 = firstPerson.GetBoneTransform(HumanBodyBones.UpperChest);*/

        thirdPersonSpine = thirdPerson.GetBoneTransform(HumanBodyBones.Spine);
        thirdPersonSpine1 = thirdPerson.GetBoneTransform(HumanBodyBones.Chest);
        thirdPersonSpine2 = thirdPerson.GetBoneTransform(HumanBodyBones.UpperChest);

        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //firstPerson.SetBool("Firing", true);
                thirdPerson.SetBool("Firing", true);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                //firstPerson.SetBool("Firing", false);
                thirdPerson.SetBool("Firing", false);
            }
        }
    }

    private void LateUpdate()
    {
        if (photonView.IsMine)
        {
            float rotationY = Input.GetAxis("Mouse Y");
            float rotationX = Input.GetAxis("Mouse X");

            if (thirdPersonAimX - rotationY * multiply > maxRotation)
            {
                firstPersonAimX = maxRotation * firstPersonLookMultiply;
                thirdPersonAimX = maxRotation;
            }
            else if (thirdPersonAimX - rotationY * multiply < minRotation)
            {
                firstPersonAimX = minRotation * firstPersonLookMultiply;
                thirdPersonAimX = minRotation;
            }
            else
            {
                firstPersonAimX -= rotationY * multiply * firstPersonLookMultiply;
                thirdPersonAimX -= rotationY * multiply;
            }

            aimY += rotationX;


            firstPersonSpine.rotation = Quaternion.Euler(firstPersonAimX, aimY, 0);
            firstPersonSpine1.rotation = Quaternion.Euler(firstPersonAimX, aimY, 0);
            firstPersonSpine2.rotation = Quaternion.Euler(firstPersonAimX, aimY, 0);

            transform.rotation = Quaternion.Euler(0, aimY, 0);

        }
        else
        {
            thirdPersonSpine.Rotate(new Vector3(cloneX, 0, 0));
            thirdPersonSpine1.Rotate(new Vector3(cloneX, 0, 0));
            thirdPersonSpine2.Rotate(new Vector3(cloneX, 0, 0));
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(thirdPersonAimX);
        }
        else if (stream.IsReading)
        {
            cloneX = (float)stream.ReceiveNext();
        }
    }
}
