using UnityEngine;

public class WeaponBullet : MonoBehaviour
{
    public string target;
    public float dmg = 0;
    public string playerId = "default";

    private void OnTriggerEnter(Collider other)
    {
        /*if(other.gameObject.tag == target)
        {
            CharacterStates state = other.gameObject.GetComponent<CharacterStates>();

            if(state == null)
            {
                return;
            }
            state.GotShot(dmg, playerId);
        }
        Destroy(gameObject);*/
    }
}
