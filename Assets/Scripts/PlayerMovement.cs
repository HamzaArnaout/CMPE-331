using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement")]
    public float speed;
    public float jumpingPower;

    public InputManager inputManager;
    private Transform cameraTransform;

    public bool canMove = true;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (canMove)
        {
            Vector2 movement = inputManager.GetPlayerMovement();
            Vector3 move = new Vector3(movement.x, 0f, movement.y);

            move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
            move.y = 0f;

            rb.AddForce(move * speed * 10f, ForceMode.Force);
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpingPower, rb.velocity.z);
        }

        if(context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.5f, rb.velocity.z);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Limit the players velocity if needed
        if(flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
