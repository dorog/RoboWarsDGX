using UnityEngine;
using Photon.Pun;

public class CharacterMovement : MonoBehaviourPun
{
    public  CharacterStats characterStat;

    public Animator firstPersonAnimator;
    public Animator thirdPersonAnimator;

    public Rigidbody body;

    private float speed = 10.0f;
    private float jumpPower = 8.0f;
    private bool grounded = false;

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

                /*firstPersonAnimator.SetFloat("Vertical", forwardMovement);
                firstPersonAnimator.SetFloat("Horizontal", sideMovement);*/

                thirdPersonAnimator.SetFloat("Vertical", forwardMovement);
                thirdPersonAnimator.SetFloat("Horizontal", sideMovement);

                if (grounded && Input.GetButton("Jump"))
                {
                    body.velocity = new Vector3(velocity.x, jumpPower, velocity.z);
                    //firstPersonAnimator.SetBool("jump", true);
                    thirdPersonAnimator.SetBool("jump", true);
                }
            }
        }

    }

    public void OnGround()
    {
        grounded = true;
        //firstPersonAnimator.SetBool("jump", false);
        thirdPersonAnimator.SetBool("jump", false);
    }

    public void InAir()
    {
        grounded = false;
        //TODO: leesik valahonnan ?
    }
}
