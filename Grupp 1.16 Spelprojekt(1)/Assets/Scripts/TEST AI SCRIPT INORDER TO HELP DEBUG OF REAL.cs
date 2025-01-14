using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTAISCRIPTINORDERTOHELPDEBUGOFREAL : MonoBehaviour
{

    public float maxWalkSpeed = 4f;  // Walk speed
    public float sprintSpeed = 8f;    // Sprint speed
    public bool isRunning = false;   // Is the player sprinting?
    public Vector2 inputOfMoving;    // Input for movement direction
    private Rigidbody2D rigid2d;     // Reference to Rigidbody2D component

    void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component
    }

    void Update()
    {
        HandleInput();   // Handle the player's movement input
    }

    void FixedUpdate()
    {
        ApplyMovement(); // Apply the movement every fixed frame
    }

    void HandleInput()
    {
        // Get player movement input (e.g., WASD or Arrow Keys)
        inputOfMoving.x = Input.GetAxisRaw("Horizontal");
        inputOfMoving.y = Input.GetAxisRaw("Vertical");

        // Check if the player is holding shift to sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    void ApplyMovement()
    {
        // Determine the target speed based on whether the player is sprinting
        float targetSpeed = isRunning ? sprintSpeed : maxWalkSpeed;

        // Calculate the target velocity based on input direction and target speed
        Vector2 targetVelocity = inputOfMoving.normalized * targetSpeed;

        // Directly set the Rigidbody2D's velocity (instant movement)
        rigid2d.velocity = new Vector2(targetVelocity.x, targetVelocity.y);
    }
}