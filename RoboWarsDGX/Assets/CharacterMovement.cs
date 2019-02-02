using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float sideSpeed = 10f;
    [SerializeField]
    private float rotationSpeed = 10;

    private Rigidbody characterRigidBody;

    void Start()
    {
        characterRigidBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        float movement = Input.GetAxis("Vertical");
        float side = Input.GetAxis("Horizontal");

        characterRigidBody.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * speed * movement +  transform.right * Time.fixedDeltaTime * sideSpeed * side);


        float yRot = Input.GetAxis("Mouse X");
        Vector3 rotation = new Vector3(0, yRot, 0);

        characterRigidBody.MoveRotation(characterRigidBody.rotation * Quaternion.Euler(rotation));
    }
}
