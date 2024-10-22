using UnityEngine;

public class EnemyBehavior : EnemyState
{
    [Header("Player Detection and Movement")]
    public float moveSpeed = 2f; 
    public float chaseSpeed = 4f;
    public Vector2 moveDirection = Vector2.right;
    public float patrolDistance = 5f;
    private Vector2 startPosition;
    private Transform player; // Changed from public to private

    [Header("Attack and Jump")]
    public float DetectionAttack = 1.5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;

    [Header("Ground Check")]
    public Transform groundCheckPoint;  // จุดตรวจสอบพื้น
    public float groundCheckRadius = 0.2f;  // รัศมีวงกลมตรวจสอบ
    public LayerMask groundLayer;  // เลเยอร์ของพื้นดิน
    public Transform rayPointG;
    public float rayDistanceG;

    protected bool isGrounded;

    private void Start()
    {
        startPosition = transform.position;
        currentState = State.Move;
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Find player by tag
        CurrentHealth = maxHP;
    }

    private void Update()
    {
        // Update player reference if it is null
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform; // Update reference if player was lost
        }

        base.Update();
        groundCheck();
    
        // Check if the player exists
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Check if the player is within the attack distance
            if (distanceToPlayer <= DetectionAttack)
            {
                currentState = State.Attack; // Change state to Attack when player is in range
            }
            else if (distanceToPlayer <= detectionRange)
            {
                currentState = State.Chase; // Change state to Chase when player is within detection range
            }
            else
            {
                currentState = State.Move; // Change state to Move when player is out of detection range
            }
        }
        else
        {
            // If player is null, switch state to Move and continue moving
            currentState = State.Move; // Set state to Move when player is gone
        }
    }

    protected override void MoveBehavior()
    {
        // Basic movement logic
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Patrol logic
        if (Vector2.Distance(startPosition, transform.position) >= patrolDistance)
        {
            moveDirection = -moveDirection; // Reverse direction when patrol distance is reached
            startPosition = transform.position; // Update the start position after reversing
        }
    }

    protected override void ChaseBehavior()
    {
        // Return early if player is null
        if (player == null)
        {
            return;
        }

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the enemy collides with the player while in Attack state
        if (collision.gameObject.CompareTag("Player") && currentState == State.Attack)
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(Damage); // Deal damage to the player

                // Destroy the player if health drops to zero or below
                if (playerStats.CurrentHealth <= 0)
                {
                    //Destroy(collision.gameObject);  // Destroy player GameObject
                }
            }
        }
        
        // Handle collision with Bullet (enemy reflects back)
        /*else if (collision.gameObject.CompareTag("Bullet"))
        {
            Vector2 reflectDirection = (transform.position - collision.transform.position).normalized;
            transform.Translate(reflectDirection * 1);
        }*/
        
        // Handle collision with Box (enemy changes direction)
        else if (collision.gameObject.CompareTag("Ground"))
        {
            HandleDirectionChange(collision);
        }
    }


    private void HandleDirectionChange(Collision2D collision)
    {
        // Check if the enemy hits something on the left or right
        if (collision.contacts[0].normal.x > 0) // Hit from the left side
        {
            moveDirection = Vector2.right; // Move right
        }
        else if (collision.contacts[0].normal.x < 0) // Hit from the right side
        {
            moveDirection = Vector2.left; // Move left
        }
    }

    protected override void AttackBehavior()
    {
        JumpTowardsPlayer();
    }

    private void JumpTowardsPlayer()
    {
        // Return early if player is null
        if (player == null)
        {
            return;
        }

        if (isGrounded)
        {
            Vector2 jumpDirection = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(jumpDirection.x * moveSpeed, jumpForce);
            //Debug.Log("Jumping towards Player!");
            isGrounded = false; // Set isGrounded to false after jumping
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

    public void OnDrawGizmos()
    {
        base.OnDrawGizmosSelected();
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DetectionAttack);
    }
}