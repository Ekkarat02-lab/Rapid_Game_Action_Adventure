using UnityEngine;

public class EnemyBehavior : EnemyState
{
    [Header("Player Detection and Movement")]
    public float moveSpeed = 2f;
    public float chaseSpeed = 4f;
    public Vector2 moveDirection = Vector2.right;
    public float patrolDistance = 5f;
    private Vector2 startPosition;
    private Transform player;

    [Header("Attack and Jump")]
    public float DetectionAttack = 1.5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;

    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform rayPointG;
    public float rayDistanceG;

    protected bool isGrounded;

    private void Start()
    {
        startPosition = transform.position;
        currentState = State.Move;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        CurrentHealth = maxHP;
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        base.Update();
        groundCheck();

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= DetectionAttack)
            {
                currentState = State.Attack; // Attack state when within DetectionAttack range
            }
            else if (distanceToPlayer <= detectionRange)
            {
                currentState = State.Chase; // Chase state when within detection range
            }
            else
            {
                currentState = State.Move; // Move state when out of detection range
            }
        }
        else
        {
            currentState = State.Move;
        }
    }

    protected override void MoveBehavior()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (Vector2.Distance(startPosition, transform.position) >= patrolDistance)
        {
            moveDirection = -moveDirection;
            startPosition = transform.position;
            FlipCharacter();
        }
    }

    private void FlipCharacter()
    {
        Vector3 currentScale = transform.localScale;
        if (moveDirection.x < 0)
        {
            currentScale.x = -Mathf.Abs(currentScale.x);
        }
        else if (moveDirection.x > 0)
        {
            currentScale.x = Mathf.Abs(currentScale.x);
        }
        transform.localScale = currentScale;
    }

    protected override void ChaseBehavior()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        FlipTowardsPlayer();
    }

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
        if (collision.gameObject.CompareTag("Player") && currentState == State.Attack)
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(Damage); 
            }
        }
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
        if (player == null) return;

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