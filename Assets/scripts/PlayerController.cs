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

    private int count;
    private bool movingBackwards = false;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        count = 0;
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

        // If down arrow key is pressed, rotate character 180 degrees
        if (movingBackwards && count>60)
        {
            transform.Rotate(Vector3.up, 180f);
            count = 0; //reset count
        }

        //jumping
        if (Input.GetButton("Jump") && playerGrounded)
        {
            movementDirection.y = jumpSpeed;
        }
        movementDirection.y -= gravity * Time.deltaTime;

        characterController.Move(movementDirection * Time.deltaTime);

        // Apply dancing when the "A" key is pressed
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
    }
}