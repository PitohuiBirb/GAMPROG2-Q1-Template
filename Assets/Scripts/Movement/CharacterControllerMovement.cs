using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;

    [SerializeField] float jumpHeight = .3f;

    private float velocity;

    [SerializeField]
    private float gravityScale = .4f;

    private float gravity = -9.8f;

    private CharacterController characterController;

    private float height;

    public Transform groundCheck;
    public float groundDistance = .3f;
    public LayerMask groundMask;

    private bool IsGrounded;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
       float xMove = Input.GetAxis("Horizontal");
       float zMove = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.right * xMove) + (transform.forward * zMove);
        moveDirection.y += velocity + gravity * Time.deltaTime * gravityScale;
        moveDirection *= moveSpeed * Time.deltaTime;
        velocity += gravity * gravityScale * Time.deltaTime;
        //Debug.Log(IsGrounded);

        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetButtonDown("Jump")&&IsGrounded)
        {
            velocity = Mathf.Sqrt(jumpHeight * -2 * (gravity * gravityScale));
        }



        characterController.Move(moveDirection);
    }
}
