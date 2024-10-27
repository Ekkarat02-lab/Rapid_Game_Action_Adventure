using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Public Variables
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float smoothTime = 0.3f;

    [Header("Ground Check Settings")]
    public Transform groundCheckPoint;  
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    
    [Header("Jump System")]
    public Transform rayPointG;
    public float rayDistanceG;
    private Vector2 gravity;
    [SerializeField] private float jumpMultiplier = 2f;
    [SerializeField] private float fallMultiplier = 2f;
    [SerializeField] private float jumpTime;

    // Protected Variables
    protected bool isGrounded;
    protected Vector2 currentVelocity = Vector2.zero;

    // Private Variables
    private bool facingRight = true;
    private Rigidbody2D rb;
    private Animator animator;
    private float jumpcounter;
    private bool isJumping;

    public void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found! Ensure it's attached to the GameObject.");
        }
        gravity = new Vector2 (0, -Physics2D.gravity.y);
    }

    void Update()
    {
        float horizontalInput = 0f;
        groundCheck();  // ตรวจสอบการชนกับพื้น

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }

        Move(horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            jumpcounter = 0f;

            if(rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
            }
        }

        if (rb.velocity.y > 0f && isJumping)
        {
            jumpcounter += Time.deltaTime;
            if (jumpcounter > jumpTime)
            {
                isJumping = false;
            }

            float t = jumpcounter / jumpTime;
            float currentJumpM = jumpMultiplier;

            if(t > 0.5f)
            {
                currentJumpM = jumpMultiplier *(1 - t);
            }

            rb.velocity += gravity * currentJumpM * Time.deltaTime;
        }

        if (rb.velocity.y < 0f)
        {
            rb.velocity -= gravity * fallMultiplier * Time.deltaTime;
        }


        FlipCharacterTowardsCursor();

    }

    public void Move(float horizontalInput)
    {
        if (rb == null) return;
        
        float targetVelocityX = horizontalInput * moveSpeed;
        float newVelocityX = Mathf.SmoothDamp(rb.velocity.x, targetVelocityX, ref currentVelocity.x, smoothTime);
        rb.velocity = new Vector2(newVelocityX, rb.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(newVelocityX));
        
        FlipCharacter(horizontalInput);
    }

    void FlipCharacterTowardsCursor()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (mousePos.x < transform.position.x && facingRight)
        {
            Flip();
        }
        RotateGunTowardsCursor();
    }

    private void FlipCharacter(float horizontalInput)
    {
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }
        
    }
    void RotateGunTowardsCursor()
    {
        Transform armTransform = transform.Find("Pivot");
        // Get the mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get the direction from the gun to the mouse
        Vector3 direction = mousePos - armTransform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the arm
        if (facingRight)
        {
            armTransform.rotation = Quaternion.Euler(0f, 0f, angle);  // No flip
        }
        else
        {
            // When the player is flipped, adjust the angle to keep the arm correct
            armTransform.rotation = Quaternion.Euler(180f, 0f, -angle);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            isJumping = true;
            jumpcounter = 0;
        }

    }

    public void groundCheck()
    {
        // ตรวจสอบการชนของผู้เล่นกับพื้นดินโดยใช้ OverlapCircle
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        // เพิ่ม Debug เพื่อช่วยตรวจสอบใน Scene
        Debug.DrawRay(rayPointG.position, Vector2.down * rayDistanceG, Color.red);
    }

    private void OnDrawGizmosSelected()
    {
        // แสดงรัศมีของ OverlapCircle ใน Scene เพื่อดูจุดตรวจสอบ
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Handle triggers here if needed
    }
}