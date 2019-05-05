using UnityEngine;
using Photon.Pun;

public class TestMovement : MonoBehaviourPun
{
    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Forward;
    public KeyCode Backward;

    [SerializeField]
    private float forwardMoveSpeed = 5;
    [SerializeField]
    private float sideMoveSpeed = 3;

    [SerializeField]
    private float jumpPower = 10f;

    private Rigidbody body;

    public GameObject cam;
    private Animator animator;
    private Animation Animation;

    private bool grounded = true;

    private float rotationX = 0f;
    private float rotationY = 0f;
    private float forwardMovement = 0f;
    private float sideMovement = 0f;
    private bool jumped = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        if (photonView.IsMine)
        {
            cam.SetActive(true);
        }
        else
        {
            cam.SetActive(false);
        }
    }

    private void Update()
    {
        rotationX = Input.GetAxis("Mouse X");
        rotationY = Input.GetAxis("Mouse Y");
        forwardMovement = Input.GetAxis("Vertical");
        sideMovement = Input.GetAxis("Horizontal");
        jumped = Input.GetKeyDown(KeyCode.Space);
    }

    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            CharacterRotation();

            /*if (!grounded)
            {
                return;
            }
            if (jumped)
            {
                Debug.Log("" + body.velocity + " " + new Vector3(0, jumpPower, 0));
                body.AddForce(body.velocity + new Vector3(0, jumpPower, 0), ForceMode.VelocityChange);
                animator.SetBool("jump", true);
                jumped = false;
                return;
            }



            //CharacterMovement();
            float side = Input.GetAxis("Horizontal");
            float forward = Input.GetAxis("Vertical");

            Vector3 targetVelocity = new Vector3(side * sideMoveSpeed, 0, forward * forwardMoveSpeed);
            targetVelocity = transform.TransformDirection(targetVelocity);

            Vector3 velocity = body.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.y = 0;
            //Debug.Log(velocityChange);
            body.AddForce(velocityChange, ForceMode.VelocityChange);
            body.velocity = velocityChange;


            animator.SetFloat("Vertical", forwardMovement);
            animator.SetFloat("Horizontal", sideMovement);*/

            if (grounded)
            {
                // Calculate how fast we should be moving
                Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                targetVelocity = transform.TransformDirection(targetVelocity);
                targetVelocity *= forwardMoveSpeed;

                // Apply a force that attempts to reach our target velocity
                //Debug.Log(body.velocity);
                Vector3 velocity = body.velocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -5, 5);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -5, 5);
                velocityChange.y = 0;
                body.AddForce(velocityChange, ForceMode.VelocityChange);

                animator.SetFloat("Vertical", forwardMovement);
                animator.SetFloat("Horizontal", sideMovement);

                // Jump
                if (grounded && Input.GetButton("Jump"))
                {
                    body.velocity = new Vector3(velocity.x, jumpPower, velocity.z);
                    Debug.Log(body.velocity);
                }
            }

            // We apply gravity manually for more tuning control
            //body.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));

            //grounded = false;
        }
    }

    private void CharacterRotation()
    {
        gameObject.transform.Rotate(new Vector3(0, rotationX, 0));
        cam.transform.Rotate(new Vector3(-rotationY, 0, 0));

        rotationX = 0;
        rotationY = 0;
    }

    private void CharacterMovement()
    {
        Vector3 targetVelocity = new Vector3(sideMovement*sideMoveSpeed, 0, forwardMovement*forwardMoveSpeed);
        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector3 velocity = body.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.y = 0;
        //Debug.Log(velocityChange);
        body.AddForce(velocityChange, ForceMode.VelocityChange);
        body.velocity = velocityChange;

        
        animator.SetFloat("Vertical", forwardMovement);
        animator.SetFloat("Horizontal", sideMovement);

        forwardMovement = 0f;
        sideMovement = 0f;
    }

    public void OnGround()
    {
        grounded = true;
        animator.SetBool("jump", false);
        //Animation["wawv"].layer = 1;
    }

    public void InAir()
    {
        grounded = false;
        //TODO: leesik valahonnan ?
    }
}
