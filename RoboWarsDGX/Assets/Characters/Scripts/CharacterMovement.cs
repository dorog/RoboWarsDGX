using UnityEngine;
using Photon;
using Photon.Pun;

public class CharacterMovement : MonoBehaviourPun
{

    public float speed = 10.0f;
    public float jumpPower = 8.0f;
    public float gravity = 20.0f;
    public Camera cam;

    private Animator animator;

    private Rigidbody body;
    private bool grounded = false;

    void Start()
    {
        if (!photonView.IsMine)
        {
            cam.gameObject.SetActive(false);
        }
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            CharacterRotation();
        }
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            if (grounded)
            {
                float forwardMovement = Input.GetAxis("Vertical");
                float sideMovement = Input.GetAxis("Horizontal");
                Vector3 targetVelocity = new Vector3(sideMovement, 0, forwardMovement);
                targetVelocity = transform.TransformDirection(targetVelocity);
                targetVelocity *= speed;

                Vector3 velocity = body.velocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.y = 0;
                body.AddForce(velocityChange, ForceMode.VelocityChange);

                animator.SetFloat("Vertical", forwardMovement);
                animator.SetFloat("Horizontal", sideMovement);

                if (grounded && Input.GetButton("Jump"))
                {
                    body.velocity = new Vector3(velocity.x, jumpPower, velocity.z);
                    animator.SetBool("jump", true);
                }
            }
        }

    }

    private void CharacterRotation()
    {
        float rotationX = Input.GetAxis("Mouse X");
        float rotationY = Input.GetAxis("Mouse Y");

        gameObject.transform.Rotate(new Vector3(0, rotationX, 0));
        cam.transform.Rotate(new Vector3(-rotationY, 0, 0));
    }

    public void OnGround()
    {
        grounded = true;
        animator.SetBool("jump", false);
    }

    public void InAir()
    {
        grounded = false;
        //TODO: leesik valahonnan ?
    }
}
