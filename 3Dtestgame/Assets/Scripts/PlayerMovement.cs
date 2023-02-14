using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController playerController;

    public float moveSpeed = 10f;
    public float jumpHeight = 3f;
    public bool headbob = true;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Animator animator;

    Vector3 playerGravityVelocity;
    Vector2 playerMovement;

    bool isGrounded;
    bool isSprinting;

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
            playerGravityVelocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
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
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isGrounded && playerGravityVelocity.y < 0)
        {
            playerGravityVelocity.y = -2f;
        }

        //Vector3 move = transform.right * playerMovement.x + transform.forward * playerMovement.y;

        playerController.Move(((transform.right * playerMovement.x) + (transform.forward * playerMovement.y)) * (isSprinting ? (moveSpeed * 1.5f) : moveSpeed) * Time.deltaTime);

        #region Headbob Animation
        if (headbob)
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
        #endregion

        playerGravityVelocity.y += Physics.gravity.y * Time.deltaTime;

        playerController.Move(playerGravityVelocity * Time.deltaTime);
    }
}
