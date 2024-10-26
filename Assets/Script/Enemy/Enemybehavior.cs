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

            // Flip character when direction changes
            FlipCharacter();
        }
    }
    
    // Flip the character based on moveDirection without changing its size
    private void FlipCharacter()
    {
        Vector3 currentScale = transform.localScale; // Get the current scale

        // Check if we are moving left (moveDirection.x < 0) or right (moveDirection.x > 0)
        if (moveDirection.x < 0)
        {
            currentScale.x = -Mathf.Abs(currentScale.x); // Flip to face left, keep the same size
        }
        else if (moveDirection.x > 0)
        {
            currentScale.x = Mathf.Abs(currentScale.x); // Flip to face right, keep the same size
        }

        transform.localScale = currentScale; // Apply the adjusted scale
    }
    
    protected override void ChaseBehavior()
    {
        // Return early if player is null
        if (player == null)
        {
            return;
        }

        // Calculate the direction towards the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

        // Flip character to face the player
        FlipTowardsPlayer();
    }

    // Flip the character to face the player
    private void FlipTowardsPlayer()
    {
        Vector3 currentScale = transform.localScale;

        if (player.position.x < transform.position.x)
        {
            currentScale.x = -Mathf.Abs(currentScale.x);
        }
        else if (player.position.x > transform.position.x)
        {
            currentScale.x = Mathf.Abs(currentScale.x);
        }
        transform.localScale = currentScale;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the enemy collides with the player while in Attack state
        if (collision.gameObject.CompareTag("Player") && currentState == State.Attack)
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(Damage); 
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
        if (collision.contacts[0].normal.x > 0)
        {
            moveDirection = Vector2.right;
        }
        else if (collision.contacts[0].normal.x < 0)
        {
            moveDirection = Vector2.left;
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
            isGrounded = false; 
        }
    }
    
    public void groundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        Debug.DrawRay(rayPointG.position, Vector2.down * rayDistanceG, Color.red);
    }

    private void OnDrawGizmosSelected()
    {
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