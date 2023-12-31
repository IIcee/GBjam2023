using System;
using GBTemplate;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float speedAdjuster;
    [SerializeField] float jumpingPower;
    [SerializeField] float groundCheckRadius;

    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip landSound;

    [SerializeField] MainManager mainManager;

    private GBConsoleController gb;
    private Rigidbody2D rb;
    [SerializeField] GameObject groundCheck;
    private SpriteRenderer playerSprite;
    private Animator playerAn;

    bool isGamePaused = false;
    bool isOnGround = true;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] Vector2 resetPos;

    //Fetching all the components.
    void Start()
    {
        gb = GBConsoleController.GetInstance();
        mainManager = MainManager.Instance;
        rb = GetComponent<Rigidbody2D>();

        playerSprite = GetComponentInChildren<SpriteRenderer>();
        playerAn = GetComponent<Animator>();
    }

    //Update manages pausing.
    void Update()
    {
        if (gb.Input.ButtonBJustPressed)
        {
            //switch these two for insta reset vs pause screen
            PauseToggler();
            //mainManager.ResetScene();
        }

        //IsGrounded();
    }

    //FixedUpdate manages player movement and animation.
    void FixedUpdate()
    {
        if (!isGamePaused)
        {
            Movement();
            playerAnimation();
        }
    }

    //Movement manages inputs and movement.
    //Old movement scripts are commented out, REMOVE later
    private void Movement()
    {
        if (gb.Input.Left)
        {
            rb.AddForce(speed * Vector2.left / speedAdjuster, ForceMode2D.Impulse);

            //rb.velocity = new Vector2(rb.velocity.x - speed, rb.velocity.y);
            //rb.velocity += speed * Vector2.left / speedAdjuster;
            //transform.position += gb.Input.LeftPressedTime * speed * Time.deltaTime * -Vector3.right;
            
            playerSprite.flipX = true;
        }

        if (gb.Input.Right)
        {
            rb.AddForce(speed * Vector2.right / speedAdjuster, ForceMode2D.Impulse);

            //rb.velocity += speed * Vector2.right / speedAdjuster;
            //transform.position += gb.Input.RightPressedTime * speed * Time.deltaTime * transform.right;

            playerSprite.flipX = false;
        }

        //You jump only when you're on ground.
        if (gb.Input.Up && isOnGround)
        {
            gb.Sound.PlaySound(jumpSound);
            isOnGround = false;
            rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);

            //rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
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

    //Checks if player is on ground using the overlap of a separate empty object with an environment object which is in the "ground" layer.
    //Currently unused, REMOVE if not using.
    private bool IsGrounded()
    {
        Debug.Log(groundCheck.transform.position);
        return Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);
    }

    //Draw gizmos when selected
    void OnDrawGizmosSelected()
    {
        // Display the isGrounded circle when selected
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawSphere(groundCheck.transform.position, groundCheckRadius);
    }

    //OnCollisionEnter2D currently manages landing.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        gb.Sound.PlaySound(landSound);
        isOnGround = true;
    }
}