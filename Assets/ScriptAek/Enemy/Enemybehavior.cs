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
    public Transform rayPointG;
    public float rayDistanceG;
    private int groundLayerIndex;
    protected bool isGrounded;

    private void Start()
    {
        startPosition = transform.position;
        currentState = State.Move;
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component
        groundLayerIndex = LayerMask.NameToLayer("groundLayer");
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Find player by tag
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
        // Move the enemy using moveDirection and moveSpeed
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Reverse direction if patrol distance is reached
        if (Vector2.Distance(startPosition, transform.position) >= patrolDistance)
        {
            moveDirection = -moveDirection; // Reverse direction when patrolDistance is reached
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
        if (collision.gameObject.CompareTag("Player") && currentState == State.Attack)
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.CurrentHealth -= 1;
            }
            if(playerStats.CurrentHealth == 0)
            {
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            Vector2 reflectDirection = (transform.position - collision.transform.position).normalized;
            transform.Translate(reflectDirection * 1);
        }
        else if (collision.gameObject.CompareTag("Box")) // Check for BoxCollider collision
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
        if (rayPointG == null)
        {
            return; // Exit the method if rayPointG is null
        }

        RaycastHit2D hit = Physics2D.Raycast(rayPointG.position, Vector2.down, rayDistanceG);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == groundLayerIndex)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        else
        {
            isGrounded = false;
        }
        Debug.DrawRay(rayPointG.position, Vector2.down * rayDistanceG, Color.red);
    }

    public void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DetectionAttack);
    }
}