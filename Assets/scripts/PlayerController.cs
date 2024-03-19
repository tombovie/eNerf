using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : MonoBehaviour
{

    private CharacterController characterController;
    private Animator animator;

    [SerializeField]
    private float movementSpeed, rotationSpeed, jumpSpeed, gravity;

    private Vector3 movementDirection = Vector3.zero;
    private bool playerGrounded;

    private bool isWalkingBackwards = false;
    private int count = 0;
    private bool movingBackwards = false;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerGrounded = characterController.isGrounded;

        //movement
        Vector3 inputMovement = transform.forward * movementSpeed * Input.GetAxisRaw("Vertical");

        if (Vector3.Dot(transform.forward, inputMovement) < 0)
        {
            movingBackwards = true;
        }
        else
        {
            movingBackwards = false;
        }



        if (movingBackwards == false)
        {
            characterController.Move(inputMovement * Time.deltaTime);
        }

        transform.Rotate(Vector3.up * Input.GetAxisRaw("Horizontal") * rotationSpeed);

        count++;
        if (inputMovement != Vector3.zero && count > 30)
        {
            isWalkingBackwards = false; //Reset trigger
        }

        // If down arrow key is pressed, rotate character 180 degrees
        if (movingBackwards && isWalkingBackwards == false)
        {
            transform.Rotate(Vector3.up, 180f);
            isWalkingBackwards = true;
            count = 0; //reset count
        }

        //jumping
        if (Input.GetButton("Jump") && playerGrounded)
        {
            movementDirection.y = jumpSpeed;
        }
        movementDirection.y -= gravity * Time.deltaTime;

        characterController.Move(movementDirection * Time.deltaTime);

        // Apply dancing when the "P" key is pressed
        if (Input.GetKey(KeyCode.P))
        {
            animator.SetTrigger("isDancing"); // Trigger the "Dance" animation
        }
        else
        {
            animator.ResetTrigger("isDancing");
        }

        //animations
        animator.SetBool("isWalking", Input.GetAxisRaw("Vertical") != 0 && movingBackwards == false);
        animator.SetBool("isJumping", !characterController.isGrounded);
        //Debug.Log("on ground: " + characterController.isGrounded);
        //Debug.Log("input: " + Input.GetAxisRaw("Vertical") + " movingbackwards: " + movingBackwards);
    }
}