using UnityEngine;

public class InstantBullet : MonoBehaviour
{
    public float maxDistance = 0;
    public float dmg = 0;
    public string playerId = "Default";
    public LayerMask layerMask;

    public void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance, layerMask))
        {
            BoneColliderHit boneColliderHit = hit.collider.gameObject.GetComponent<BoneColliderHit>();
            Debug.Log(hit.collider.gameObject.name);
            // hit.point: Spawn blood
            if(boneColliderHit == null)
            {
                Debug.Log("Null");
                return;
            }

            boneColliderHit.GotShot(dmg, playerId);
        }

        Destroy(gameObject);
    }
}
