using UnityEngine;

public class GroundChecker : MonoBehaviour
{

    [SerializeField]
    private CharacterMovement movement;

    public int count = 0;

    public CharacterMovement Movement { get => movement; set => movement = value; }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        count++;
        if (count == 1)
        {
            Movement.OnGround();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.name);
        count--;
        if (count == 0)
        {
            Movement.InAir();
        }
    }
}
