using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyState
{
    [Header("Player Detection and Movement")]
    public float moveSpeed = 2f; 
    public float chaseSpeed = 4f;
    public Vector2 moveDirection = Vector2.right;
    public float patrolDistance = 5f;
    private Vector2 startPosition;
    private Transform playerFollow;
    private bool facingRight = true; // ตรวจสอบทิศทางของตัวละคร

    [Header("Animator")]
    public Animator animator; // เพิ่มตัวแปร Animator

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

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;

    private GameObject player;
    private float nextFireTime;

    private void Start()
    {
        startPosition = transform.position;
        currentState = State.Move;
        rb = GetComponent<Rigidbody2D>();  
        playerFollow = GameObject.FindGameObjectWithTag("Player")?.transform; 
        player = GameObject.FindGameObjectWithTag("Player");
        CurrentHealth = maxHP;

        // ตรวจสอบการใช้งาน Animator
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (player == null)
        {
            playerFollow = GameObject.FindGameObjectWithTag("Player")?.transform; 
        }
        
        if (player != null && Vector2.Distance(transform.position, player.transform.position) <= detectionRange)
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }

        base.Update();
        groundCheck();

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerFollow.position);

            if (distanceToPlayer <= DetectionAttack)
            {
                currentState = State.Attack;
            }
            else if (distanceToPlayer <= detectionRange)
            {
                currentState = State.Chase;
            }
            else
            {
                currentState = State.Move;
            }
        }
        else
        {
            currentState = State.Move;
        }
    }

    protected override void MoveBehavior()
    {
        // กำหนดให้เล่น Animation เดิน
        if (animator != null)
        {
            animator.SetBool("isWalking", true);
        }

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (Vector2.Distance(startPosition, transform.position) >= patrolDistance)
        {
            Flip(); // พลิกทิศทางตัวละคร
            moveDirection = -moveDirection;
            startPosition = transform.position;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    protected override void ChaseBehavior()
    {
        if (player == null)
        {
            return;
        }

        Vector2 direction = (playerFollow.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, playerFollow.position, chaseSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && currentState == State.Attack)
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(Damage); 
                if (playerStats.CurrentHealth <= 0)
                {
                    //Destroy(collision.gameObject);
                }
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
        if (player == null)
        {
            return;
        }

        if (isGrounded)
        {
            Vector2 jumpDirection = (playerFollow.position - transform.position).normalized;
            rb.velocity = new Vector2(jumpDirection.x * moveSpeed, jumpForce);
            isGrounded = false;
        }
    }

    public void groundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        Debug.DrawRay(rayPointG.position, Vector2.down * rayDistanceG, Color.red);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Vector2 direction = (player.transform.position - firePoint.position).normalized;
        bullet.GetComponent<Bullet>().Initialize(direction);
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