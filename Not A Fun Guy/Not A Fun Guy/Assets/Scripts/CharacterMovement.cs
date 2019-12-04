using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float characterSpeed = 35.0f;
    private float horizontalMovement = 0.0f;
    private bool isJumping = false;
    private bool isCrouching = false;

    private void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * characterSpeed;

        // Handles animation based on if the character is moving or not.
        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            // Activates jumping animation
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
        }
    }

    /**
    * OnLanding method gets called when the character
    * hits the ground after having previously jump.
    */
    public void OnLanding()
    {
        // Cancels jumping animation
        animator.SetBool("IsJumping", false);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMovement * Time.fixedDeltaTime, isCrouching, isJumping);
        isJumping = false;
    }
}
