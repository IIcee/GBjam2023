using GBTemplate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{
    // adding public animator for player

    private float horizontal;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpingPower = 2f;
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    private GBConsoleController gb;
    private Transform groundCheck;
    private GameObject playerObject;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        gb = GBConsoleController.GetInstance();

        playerObject = GetComponent<GameObject>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<Transform>();
    }

    void Update()
    {
        // animator logic


        horizontal = Input.GetAxisRaw("Horizontal");

        if (gb.Input.UpJustPressed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        Flip();
        IsGrounded();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2 (horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;  
            transform.localScale = localScale;
        }
    }
}