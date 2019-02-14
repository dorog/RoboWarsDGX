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

            if (!grounded)
            {
                return;
            }
            if (jumped)
            {
                body.velocity += new Vector3(0, jumpPower, 0);
                animator.SetBool("jump", true);
                jumped = false;
                return;
            }

            CharacterMovement();

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
        body.AddForce(velocityChange, ForceMode.VelocityChange);

        animator.SetFloat("Vertical", forwardMovement);
        animator.SetFloat("Horizontal", sideMovement);

        forwardMovement = 0f;
        sideMovement = 0f;
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
