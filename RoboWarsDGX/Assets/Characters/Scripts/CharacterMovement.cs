using UnityEngine;
using Photon.Pun;

public class CharacterMovement : MonoBehaviourPun
{
    public  CharacterStats characterStat;

    public Animator firstPersonAnimator;
    public Animator thirdPersonAnimator;

    public Rigidbody body;

    public SoundMaker soundMaker;

    private float speed = 10.0f;
    private float jumpPower = 8.0f;
    private bool grounded = false;
    private bool jumpSound = false;

    private MovementState state = MovementState.Idling;

    private void Start()
    {
        if (photonView.IsMine)
        {
            speed = characterStat.GetMovementSpeed();
            jumpPower = characterStat.GetJumpPower();
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

                thirdPersonAnimator.SetFloat("Vertical", forwardMovement);
                thirdPersonAnimator.SetFloat("Horizontal", sideMovement);

                if (grounded && Input.GetButton("Jump"))
                {
                    body.velocity = new Vector3(velocity.x, jumpPower, velocity.z);
                    //firstPersonAnimator.SetBool("jump", true);
                    thirdPersonAnimator.SetBool("jump", true);

                    jumpSound = true;
                }

                SoundCheck(forwardMovement, sideMovement);
            }
        }

    }

    private void SoundCheck(float forwardMovement, float sideMovement)
    {
        if(jumpSound)
        {
            jumpSound = false;
            photonView.RPC("Jump", RpcTarget.All);
        }

        if(forwardMovement == 0 && sideMovement == 0 && state == MovementState.Walking)
        {
            photonView.RPC("StoppedWalking", RpcTarget.All);
        }
        else if ((forwardMovement != 0 || sideMovement != 0) && state == MovementState.Idling)
        {
            photonView.RPC("StartWalking", RpcTarget.All);
        }
    }

    [PunRPC]
    private void StartWalking()
    {
        state = MovementState.Walking;
        soundMaker.StartedWalking();
    }

    [PunRPC]
    private void StoppedWalking()
    {
        state = MovementState.Idling;
        soundMaker.Stopped();
    }

    [PunRPC]
    private void Jump()
    {
        state = MovementState.Flying;
        soundMaker.Jumped();
    }

    public void OnGround()
    {
        grounded = true;
        //firstPersonAnimator.SetBool("jump", false);
        thirdPersonAnimator.SetBool("jump", false);

        //RPC?
        state = MovementState.Idling;
        soundMaker.Landed();
    }

    public void InAir()
    {
        grounded = false;
        //TODO: leesik valahonnan ?
    }

    private enum MovementState
    {
        Idling, Walking, Flying
    }
}
