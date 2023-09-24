using GBTemplate;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField][Range(1.0f, 10.0f)] private float speed;
    [SerializeField] float speedAdjuster;
    [SerializeField] private float jumpingPower;

    [SerializeField] AudioClip jumpSound;

    private GBConsoleController gb;
    private Rigidbody2D rb;
    private Transform groundCheck;
    private SpriteRenderer playerSprite;

    [SerializeField] private LayerMask groundLayer;

    //Fetching all the components.
    void Start()
    {
        gb = GBConsoleController.GetInstance();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<Transform>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    //Simple movement. There's no maxSpeed so the longer you hold the faster you get.
    void Update()
    {
        if (gb.Input.Left)
        {
            rb.velocity += speed * Vector2.left/speedAdjuster;
            transform.position += gb.Input.LeftPressedTime * speed * Time.deltaTime * -Vector3.right;
            playerSprite.flipX = true;
        }

        if (gb.Input.Right)
        {
            rb.velocity += speed * Vector2.right/speedAdjuster;
            transform.position += gb.Input.RightPressedTime * speed * Time.deltaTime * transform.right;
            playerSprite.flipX = false;
        }

        //You jump only when you're on ground.
        if (gb.Input.UpJustPressed && IsGrounded())
        {
            gb.Sound.PlaySound(jumpSound);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        IsGrounded();
    }

    //Checks if player is on ground using the collision of a separate empty object to an environment object which is in the "ground" layer.
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}