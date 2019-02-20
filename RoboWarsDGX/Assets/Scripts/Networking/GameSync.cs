using UnityEngine;
using Photon.Pun;

public class GameSync : MonoBehaviourPun, IPunObservable
{
    public Vector3 ObjPosition;
    public Quaternion ObjRotation;
    public Vector3 ObjScale;

    public float LerpSpeed = 3f;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(gameObject.transform.position);
            stream.SendNext(gameObject.transform.rotation);
            stream.SendNext(gameObject.transform.localScale);
        }
        else if (stream.IsReading)
        {
            ObjPosition = (Vector3)stream.ReceiveNext();
            ObjRotation = (Quaternion)stream.ReceiveNext();
            ObjScale = (Vector3)stream.ReceiveNext();
        }
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            UpdateTransform();
        }
    }

    private void UpdateTransform()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, ObjPosition, LerpSpeed * Time.deltaTime);
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, ObjRotation, LerpSpeed * Time.deltaTime);
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, ObjScale, LerpSpeed * Time.deltaTime);
    }
}
