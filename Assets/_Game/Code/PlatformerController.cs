using UnityEngine;

public class PlatformerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 7f;
    public float jumpForce = 12f;

    [Header("Sprint")]
    public float sprintMultiplier = 1.5f; // just a speed boost

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Jump Settings")]
    public int maxJumps = 2;       // 1 ground jump + 1 air jump
    public float coyoteTime = 0.2f;
    private float coyoteCounter;
    private int jumpCount;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;

    // platform parenting
    private Transform _originalParent;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // 👈 stops tipping over
        _originalParent = transform.parent;
    }

    void Update()
    {
        // Movement input
        moveInput = Input.GetAxisRaw("Horizontal");

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            jumpCount = 0;
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (coyoteCounter > 0f || jumpCount < maxJumps - 1)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpCount++;
                coyoteCounter = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        // Apply movement with sprint multiplier
        bool sprinting = Input.GetKey(KeyCode.LeftShift);
        float speed = sprinting ? moveSpeed * sprintMultiplier : moveSpeed;

        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    // --- Platform parenting methods ---
    public void SetParent(Transform newParent)
    {
        _originalParent = transform.parent;
        transform.SetParent(newParent);
    }

    public void ResetParent()
    {
        transform.SetParent(_originalParent);
    }
}
