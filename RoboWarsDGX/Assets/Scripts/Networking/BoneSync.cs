using UnityEngine;
using Photon.Pun;

public class BoneSync : MonoBehaviourPun, IPunObservable
{
    public Quaternion rotSpine;
    public Quaternion rotSpine1;
    public Quaternion rotSpine2;

    [Header ("First Person")]
    public Transform firstPersonSpine;
    public Transform firstPersonSpine1;
    public Transform firstPersonSpine2;

    [Header("Third Person")]
    public Transform thirdPersonSpine;
    public Transform thirdPersonSpine1;
    public Transform thirdPersonSpine2;

    public float LerpSpeed = 10f;

    private int readCount = 0;
    private int writeCount = 0;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            Debug.Log("Send(" + writeCount + "):" + firstPersonSpine.rotation);
            writeCount++;
            stream.SendNext(firstPersonSpine.rotation);
            stream.SendNext(firstPersonSpine1.rotation);
            stream.SendNext(firstPersonSpine2.rotation);
        }
        else if (stream.IsReading)
        {
            rotSpine = (Quaternion)stream.ReceiveNext();
            Debug.Log("Read(" + readCount + "):" + rotSpine);
            rotSpine1 = (Quaternion)stream.ReceiveNext();
            rotSpine2 = (Quaternion)stream.ReceiveNext();
        }
    }

    void LateUpdate()
    {
        if (!photonView.IsMine)
        {
            UpdateTransform();
        }
    }

    private void UpdateTransform()
    {
        thirdPersonSpine.rotation = firstPersonSpine.rotation;
        thirdPersonSpine1.rotation = firstPersonSpine1.rotation;
        thirdPersonSpine2.rotation = firstPersonSpine2.rotation;
    }
}
