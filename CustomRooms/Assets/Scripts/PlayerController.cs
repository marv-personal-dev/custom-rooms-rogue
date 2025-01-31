using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2.0f;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ensure Rigidbody2D is set up correctly
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Get input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize to prevent faster diagonal movement
        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }

        // Determine if player is moving
        bool isMoving = movement != Vector2.zero;
        animator.SetBool("IsMoving", isMoving);

        if (isMoving)
        {
            // Set animation direction
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);

            // Handle sprite flipping
            if (movement.x > 0.1f) // Moving Right
            {
                spriteRenderer.flipX = true; // Flip the Left animation
            }
            else if (movement.x < -0.1f) // Moving Left
            {
                spriteRenderer.flipX = false; // Keep original orientation
            }
        }
        else
        {
            // If not moving, go back to idle
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
        }
    }

    void FixedUpdate()
    {
        // Move the character
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}