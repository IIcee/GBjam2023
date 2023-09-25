using System;
using GBTemplate;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField][Range(1.0f, 10.0f)] private float speed;
    [SerializeField] float speedAdjuster;
    [SerializeField] private float jumpingPower;

    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip landSound;

    [SerializeField] MainManager mainManager;

    private GBConsoleController gb;
    private Rigidbody2D rb;
    private Transform groundCheck;
    private SpriteRenderer playerSprite;
    private Animator playerAn;
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
        playerAn = GetComponent<Animator>();
    }

    //Simple movement. There's no maxSpeed so the longer you hold the faster you get.
    void LateUpdate()
    {
        if (!isGamePaused)
        {
            Movement();
            playerAnimation();
        }

        if (gb.Input.ButtonBJustPressed)
        {
            //switch these two for insta reset vs pause screen
            PauseToggler();
            //mainManager.ResetScene();
        }

        IsGrounded();
    }

    //Movement manages inputs and movement.
    private void Movement()
    {
        if (gb.Input.Left)
        {
            rb.velocity += speed * Vector2.left / speedAdjuster;
            //rb.AddForce(speed * Vector2.left / speedAdjuster, ForceMode2D.Impulse);
            //transform.position += gb.Input.LeftPressedTime * speed * Time.deltaTime * -Vector3.right;
            playerSprite.flipX = true;
        }

        if (gb.Input.Right)
        {
            //rb.AddForce(speed * Vector2.right / speedAdjuster, ForceMode2D.Impulse);
            rb.velocity += speed * Vector2.right / speedAdjuster;
            //transform.position += gb.Input.RightPressedTime * speed * Time.deltaTime * transform.right;
            playerSprite.flipX = false;
        }

        //You jump only when you're on ground.
        if (gb.Input.UpJustPressed && IsGrounded())
        {
            gb.Sound.PlaySound(jumpSound);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            //rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
        }
    }

    //Manages the player's movement animation.
    private void playerAnimation()
    {
        //allowing animation to speed up (animation time speeds up by factor of speed)
        //sqrt is good so that animation doesn't get too fast. Could switch to log to slow it more
        playerAn.SetFloat("speed", (float)Math.Sqrt(Math.Abs(rb.velocity.x)));

        /*
        basic animator if we want animation speed to be constant
        if (rb.velocity.x != 0)
        {
            playerAn.SetFloat("speed", 2);
        }
        else
        {
            playerAn.SetFloat("speed", 0);
        }
        */
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

    //Landing sound, doesn't work atm
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        gb.Sound.PlaySound(landSound);
    }
}