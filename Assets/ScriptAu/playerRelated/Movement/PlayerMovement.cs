using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Rigidbody2D rb;
    public float smoothTime = 0.3f;
    public Transform groundCheckPoint;  // จุดตรวจสอบพื้น
    public float groundCheckRadius = 0.2f;  // รัศมีวงกลมตรวจสอบ
    public LayerMask groundLayer;  // เลเยอร์ของพื้นดิน
    public Transform rayPointG;
    public float rayDistanceG;

    protected bool isGrounded;
    protected Vector2 currentVelocity = Vector2.zero;

    private bool facingRight = true;
    [SerializeField] private Vector3 gravity = new Vector3(0, -9.81f, 0);
    [SerializeField] private float gravityX = 2f;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found! Ensure it's attached to the GameObject.");
        }
        Physics2D.gravity = gravity;
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

        FlipCharacterTowardsCursor();
        
        if (!isGrounded)
        {
            gravity = new Vector3(0, -9.81f * gravityX, 0);
        }
        else
        {
            gravity = new Vector3(0, -9.81f, 0);
        }
    }

    public void Move(float horizontalInput)
    {
        if (rb == null) return;

        float targetVelocityX = horizontalInput * moveSpeed;
        float newVelocityX = Mathf.SmoothDamp(rb.velocity.x, targetVelocityX, ref currentVelocity.x, smoothTime);
        rb.velocity = new Vector2(newVelocityX, rb.velocity.y);

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