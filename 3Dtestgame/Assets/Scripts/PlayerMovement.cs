using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController playerController;

    public float moveSpeed = 10f;
    public float jumpHeight = 1.5f;
    public float sprintModifier = 1.5f;
    public bool headbob = false;

    public Transform groundCheck;
    public LayerMask groundMask;
    public Animator animator;

    Vector3 gravityVelocity;
    Vector2 playerMovement;

    private float playerXVelocity = 0f;
    private float playerYVelocity = 0f;
    private float playerAcceleration = 0.25f;

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
        UpdateMovement();

        // Gravity
        if (isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = -2f;
        }

        // Moving player
        //playerController.Move(((transform.right * playerMovement.x) + (transform.forward * playerMovement.y)) * (isSprinting ? (moveSpeed * sprintModifier) : moveSpeed) * Time.deltaTime);
        playerController.Move(((transform.right * (playerXVelocity / moveSpeed)) + (transform.forward * (playerYVelocity / moveSpeed))) * (isSprinting ? (moveSpeed * sprintModifier) : moveSpeed) * Time.deltaTime);

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
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.5f, groundMask);
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

    void UpdateMovement()
    {
        playerXVelocity += (playerMovement.x * (playerAcceleration * 2)) + (OppositeSign(playerXVelocity) * playerAcceleration);

        if (playerXVelocity > moveSpeed)
        {
            playerXVelocity = moveSpeed;
        }
        else if (playerXVelocity < (moveSpeed * -1))
        {
            playerXVelocity = moveSpeed * -1;
        }
        else if ((playerAcceleration * -1) < playerXVelocity && playerXVelocity < playerAcceleration && playerMovement.x == 0)
        {
            playerXVelocity = 0;
        }

        playerYVelocity += (playerMovement.y * (playerAcceleration * 2)) + (OppositeSign(playerYVelocity) * playerAcceleration);

        if (playerYVelocity > moveSpeed)
        {
            playerYVelocity = moveSpeed;
        }
        else if (playerYVelocity < (moveSpeed * -1))
        {
            playerYVelocity = moveSpeed * -1;
        }
        else if ((playerAcceleration * -1) < playerYVelocity && playerYVelocity < playerAcceleration && playerMovement.y == 0)
        {
            playerYVelocity = 0;
        }

        Debug.Log("Movement Vector: " + playerMovement + ", Velocity Vector: " + playerXVelocity+ ", " + playerYVelocity);
    }

    float OppositeSign(float number)
    {
        if (number > 0)
        {
            return -1f;
        }
        else if (number < 0)
        {
            return 1f;
        }
        else
        {
            return 0f;
        }
    }
}