using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController playerController;

    public float moveSpeed = 10f;
    public float jumpHeight = 1.5f;
    public bool headbob = true;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Animator animator;

    Vector3 gravityVelocity;
    Vector2 playerMovement;

    public bool isGrounded;
    public bool isSprinting;

    // Start is called before the first frame update
    void Start()
    {
        isSprinting = false;
    }

    void OnMove(InputValue value)
    {
        playerMovement = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (isGrounded)
        {
            gravityVelocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
    }

    void OnSprint()
    {
        isSprinting = !isSprinting;
        animator.SetBool("Sprinting", isSprinting);
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();

        // Gravity
        if (isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = -2f;
        }

        // Moving player
        playerController.Move(((transform.right * playerMovement.x) + (transform.forward * playerMovement.y)) * (isSprinting ? (moveSpeed * 1.5f) : moveSpeed) * Time.deltaTime);

        #region Headbob Animation
        if (headbob)
        {
            HeadbobAnimation();
        }
        #endregion

        gravityVelocity.y += Physics.gravity.y * Time.deltaTime;

        playerController.Move(gravityVelocity * Time.deltaTime);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
    }

    void HeadbobAnimation()
    {
        if ((playerController.velocity.z != 0 || playerController.velocity.x != 0) && !animator.GetBool("Moving"))
        {
            animator.SetBool("Moving", true);
        }
        else if ((playerController.velocity.z == 0 || playerController.velocity.x == 0) && animator.GetBool("Moving"))
        {
            animator.SetBool("Moving", false);
        }
    }
}
