using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The character controller that is in charge of the player.")]
    private CharacterController controller = null;

    [Header("Properties")]

    [SerializeField]
    [Tooltip("The desired move speed of the character from a walk.")]
    [Range(1f, 100f)]
    private float moveSpeed = 5f;

    public bool isSprinting = false;
    public float sprintMultiplier = 2f;

    [SerializeField]
    [Tooltip("The force of gravity applied to the player per second")]
    private float gravity = -9.81f;

    [SerializeField]
    [Tooltip("The jump height of the player.")]
    private float jumpSpeed = 5f;
    
    [Header("Player On Ground Tools")]

    [SerializeField]
    [Tooltip("The game object that will check if the player is standing on ground.\n\nNOTE: Should be on the bottom of the player model.")]
    private Transform groundChecker = null;

    [SerializeField]
    [Tooltip("The distance below the player that the hit detection checks.\n\nNOTE: This is a radius of a generated sphere.")]
    private float groundDistance = .2f;

    [SerializeField]
    [Tooltip("The layer for which the ground checker is searching for.\n\nNOTE: Anything that should be stood on, should be of part Terrain.")]
    public LayerMask groundLayer;

    public bool isMovingForwardBackward;
    public bool isMovingLeftRight;

    public float yVelocity = 0f;
    public bool isGrounded = false;
    public bool wantToJump = false;

    private void Start()
    {
        gravity *= Time.fixedDeltaTime * Time.fixedDeltaTime;
        moveSpeed *= Time.fixedDeltaTime;
        moveSpeed /= 10;
        jumpSpeed *= Time.fixedDeltaTime;
    }

    private void Update()
    {
        ListenForSprint();
        ListenForJump();

        checkIfGrounded();
        doPlayerMovement();
    }

    void FixedUpdate()
    {
        applyGravity();
    }

    private void checkIfGrounded()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundLayer);

        if (isGrounded && yVelocity < 0)
        {
            yVelocity = -2f;
        }
    }

    private void doPlayerMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxis("Vertical");

        isMovingForwardBackward = z != 0;
        isMovingLeftRight = x != 0;

        Vector3 moveVector = transform.right * x + transform.forward * z;
        moveVector *= moveSpeed;

        if (isSprinting)
        {
            moveVector *= sprintMultiplier;
        }

        if(wantToJump && isGrounded)
        {
            yVelocity = jumpSpeed;
            wantToJump = false;
        }

        moveVector.y = yVelocity;
        controller.Move(moveVector);
    }

    private void applyGravity()
    {
        yVelocity += gravity;
    }

    private void ListenForSprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }
    }

    private void ListenForJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            wantToJump = true;
        }
    }
}
