using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    [HideInInspector]
    public bool isInTransition = false;
    public float characterSpeed = 35.0f;
    private float horizontalMovement = 0.0f;
    private bool isJumping = false;
    private bool isCrouching = false;

    private AudioSource walk;
    private bool playing = false;


    private void Awake()
    {
        walk = GetComponents<AudioSource>()[1];
    }

    private void Update()
    {
        if (!isInTransition)
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal") * characterSpeed;
        } 
        else 
        {
            horizontalMovement = 1.0f * characterSpeed;
        }
        
        // Handles animation based on if the character is moving or not.
        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));

        if (Input.GetButtonDown("Jump"))
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
        if (System.Math.Abs(horizontalMovement) > 0)
        {
            if (!playing)
            {
                playing = true;
                walk.Play();
            }
            if (!CharacterController2D.m_Grounded)
            {
                playing = false;
                walk.Stop();
            }
        }
        else
        {
            playing = false;
            walk.Stop();
        }

        controller.Move(horizontalMovement * Time.fixedDeltaTime, isCrouching, isJumping);
        isJumping = false;
    }
}
