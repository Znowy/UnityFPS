using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController playerController;

    public float moveSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Animator animator;

    Vector3 playerVelocity;

    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        #region Player Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        playerController.Move(move * moveSpeed * Time.deltaTime);
        #endregion

        #region Headbob Animation
        if ((playerController.velocity.z != 0 || playerController.velocity.x != 0) && !animator.GetBool("Moving"))
        {
            animator.SetBool("Moving", true);
        }
        else if ((playerController.velocity.z == 0 || playerController.velocity.x == 0) && animator.GetBool("Moving"))
        {
            animator.SetBool("Moving", false);
        }
        #endregion

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;

        playerController.Move(playerVelocity * Time.deltaTime);
    }
}
