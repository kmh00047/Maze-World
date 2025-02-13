using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Tooltip("Amount of force applied per frame")]
    public float speed = 5f;
    [Tooltip("Max velocity of the player")]
    public float maxSpeed = 8f;
    [Tooltip("How fast the player should stop")]
    public float deceleration = 3f;
    [Tooltip("Zoom of the camera on Player")]
    public float cameraDistance = 10f;
    [Tooltip("Upward force applied for jump")]
    public float jumpForce = 35f;

    [Space]
    public Animator animator;
    public LayerMask groundMask;

    private Rigidbody2D rb;
    private bool isGrounded = true;
    private Vector2 moveInput;
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Jump.performed += OnJump;
        inputActions.Player.Jump.canceled += OnJumpRelease;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Jump.performed -= OnJump;
        inputActions.Player.Jump.canceled -= OnJumpRelease;
        inputActions.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce * 0.2f, ForceMode2D.Impulse);
            if (AudioManager.instance != null)
                AudioManager.instance.PlayJumpAudio();
            animator.Play("Scale Up");

        }
    }

    private void OnJumpRelease(InputAction.CallbackContext context)
    {
        animator.Play("Scale Down");
    }

    private void Start()
    {
        if(Shop.instance != null)
        Shop.instance.ApplySkin(gameObject);
    }
    private void Update()
    {
        float moveDirection = moveInput.x;

        if (moveDirection != 0)
        {
            rb.AddForce(new Vector2(moveDirection * speed * 120 * Time.deltaTime, 0));
        }

        // Deceleration
        if (moveDirection == 0 && isGrounded)
        {
            if (rb.linearVelocity.x < -(maxSpeed / 7))
                rb.linearVelocity += new Vector2(deceleration * Time.deltaTime, 0);
            else if (rb.linearVelocity.x > (maxSpeed / 7))
                rb.linearVelocity -= new Vector2(deceleration * Time.deltaTime, 0);
        }
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - cameraDistance);
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.x >= maxSpeed)
        {
            rb.linearVelocity = new Vector2(maxSpeed, rb.linearVelocity.y);
        }
        else if (rb.linearVelocity.x <= -maxSpeed)
        {
            rb.linearVelocity = new Vector2(-maxSpeed, rb.linearVelocity.y);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((groundMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((groundMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            isGrounded = false;
        }
    }

}
