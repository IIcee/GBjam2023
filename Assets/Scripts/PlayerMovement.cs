using System;
using GBTemplate;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField][Range(1.0f, 10.0f)] private float speed;
    [SerializeField] float speedAdjuster;
    [SerializeField] private float jumpingPower;

    [SerializeField] AudioClip jumpSound;

    [SerializeField] MainManager mainManager;

    private GBConsoleController gb;
    private Rigidbody2D rb;
    private Transform groundCheck;
    private SpriteRenderer playerSprite;
    private Transform playerReset;
    private bool isGamePaused = false;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] Vector2 resetPos;

    //Fetching all the components.
    void Start()
    {
        gb = GBConsoleController.GetInstance();
        mainManager = MainManager.Instance;
        rb = GetComponent<Rigidbody2D>();

        groundCheck = GetComponentInChildren<Transform>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        playerReset = GetComponentInChildren<Transform>();
    }

    //Simple movement. There's no maxSpeed so the longer you hold the faster you get.
    void Update()
    {
        if (!isGamePaused)
        {
            Movement();
        }

        if (gb.Input.ButtonBJustPressed)
        {
            //switch these two for insta reset vs pause screen
            PauseToggler();
            //mainManager.ResetScene();
        }

        //Out of bounds reset
        if (transform.position.y < -1)
        {
            mainManager.ResetScene();
        }

        IsGrounded();
    }

    //Movement manages inputs and movement.
    private void Movement()
    {
        if (gb.Input.Left)
        {
            rb.velocity += speed * Vector2.left / speedAdjuster;
            //transform.position += gb.Input.LeftPressedTime * speed * Time.deltaTime * -Vector3.right;
            playerSprite.flipX = true;
        }

        if (gb.Input.Right)
        {
            rb.velocity += speed * Vector2.right / speedAdjuster;
            //transform.position += gb.Input.RightPressedTime * speed * Time.deltaTime * transform.right;
            playerSprite.flipX = false;
        }

        //You jump only when you're on ground.
        if (gb.Input.UpJustPressed && IsGrounded())
        {
            gb.Sound.PlaySound(jumpSound);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }

    //Pause toggler pauses the game and brings up pause buttons
    void PauseToggler()
    {
        if (!isGamePaused)
        {
            mainManager.Pause();
        }
        else
        {
            mainManager.UnPause();
        }
        isGamePaused = !isGamePaused;
    }

    //Checks if player is on ground using the collision of a separate empty object to an environment object which is in the "ground" layer.
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}