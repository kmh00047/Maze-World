using UnityEngine;

public class Movement : MonoBehaviour
{
    [Tooltip("Amount of force applied per frame")]
    public float speed = 5f;
    [Tooltip("Max velocity of the player")]
    public float maxSpeed = 8f;
    [Tooltip("How fast the player should stop")]
    public float decelaration = 3f;
    [Tooltip("Zoom of the camera on Player")]
    public float cameraDistance = 10f;
    [Tooltip("Upward force applied for jump")]
    public float jumpForce = 35f;

    [Space]
    [Space]

    public Animator animator;
    public LayerMask groundMask;
    //public LayerMask wallMask;

    private bool isMovingRight;
    private bool isMovingLeft;
    private bool isJumping;
    private Rigidbody2D rb;
    private bool isGrounded = true;
    //private bool isTouchingWall = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Horizontal Movement
        if(isMovingRight)
        {
            rb.AddForceX(speed * 100 * Time.deltaTime);
        }

        else if (isMovingLeft)
        {
            rb.AddForceX(-speed * 100 * Time.deltaTime);
        }

        // Snap changing direction
        if (isMovingRight && (rb.linearVelocityX < 0))
        {
            rb.AddForceX(speed * 100 * Time.deltaTime);
        }

        else if (isMovingLeft && (rb.linearVelocityX > 0))
        {
            rb.AddForceX(-speed * 100 * Time.deltaTime);
        }

        // Jumping 

        if(isJumping && isGrounded)
        {
            rb.linearVelocityY = 0;
            rb.AddForceY(jumpForce * 10);
            animator.Play("Scale Up");
            isJumping = false;
        }

        

        // Deceleration 
        if ((!isMovingLeft) && (!isMovingRight) && (isGrounded))
        {
            if (rb.linearVelocityX < -(maxSpeed/7))
                rb.linearVelocityX += decelaration * Time.deltaTime;
            else if (rb.linearVelocityX > (maxSpeed/7))
            {
                rb.linearVelocityX -= decelaration * Time.deltaTime;
            }
        }
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - cameraDistance);
    }
    private void FixedUpdate()
    {
        if (rb.linearVelocityX >= maxSpeed) 
        {
            rb.linearVelocityX = maxSpeed;
        }
        else if(rb.linearVelocityX <= -maxSpeed)
        {
            rb.linearVelocityX = -maxSpeed;
        }
    }

    // Inputs

    public void OnLeftButtonDown()
    {
        isMovingLeft = true;
    }

    public void OnRightButtonDown()
    {
        isMovingRight = true;
    }

    public void OnLeftButtonUp()
    {
        isMovingLeft = false;
    }

    public void OnRightButtonUp()
    {
        isMovingRight = false;
    }

    /// Jumping
    public void OnJumpButtonPressed()
    {
        if(isGrounded)
        { isJumping = true; }
    }

    public void OnJumpButtonUp()
    {
        //animator.StopPlayback();
        animator.Play("Scale Down");
        //isJumping = false;
    }

    // Collision Checking
    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((groundMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            isGrounded = true;
            ///Debug.Log("Player is On the ground.");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((groundMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            isGrounded = false;
            ///Debug.Log("Player is in Hawwaaaa.");
        }
    }

}
