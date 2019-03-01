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
            CharacterStates characterStates = hit.collider.gameObject.GetComponent<CharacterStates>();
            // hit.point: Spawn blood
            if(characterStates == null)
            {
                return;
            }

            characterStates.GotShot(dmg, playerId);
        }

        Destroy(gameObject);
    }
}
