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
    private bool facingRight = true;

    [Header("Animator")]
    public Animator animator;

    [Header("Attack")]
    public float DetectionAttack = 1.5f; // ระยะการโจมตี

    [Header("Ground Check")]
    public Transform groundCheckPoint;  
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    protected bool isGrounded;
    private GameObject player;

    private void Start()
    {
        startPosition = transform.position;
        currentState = State.Move; // เริ่มต้นที่ State.Move
        playerFollow = GameObject.FindGameObjectWithTag("Player")?.transform; 
        player = GameObject.FindGameObjectWithTag("Player");
        CurrentHealth = maxHP;
    }

    private void Update()
    {
        base.Update(); // เรียกใช้ฟังก์ชัน Update ของ EnemyState

        groundCheck(); // ตรวจสอบว่ามีการสัมผัสพื้น

        if (player == null)
        {
            playerFollow = GameObject.FindGameObjectWithTag("Player")?.transform; 
        }

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerFollow.position);

            if (distanceToPlayer <= DetectionAttack)
            {
                currentState = State.Attack;
                animator.SetBool("Attack", true); // เปิดแอนิเมชัน Attack
            }
            else
            {
                animator.SetBool("Attack", false); // ปิดแอนิเมชัน Attack
                if (distanceToPlayer <= detectionRange) // ใช้ detectionRange จากคลาสแม่
                {
                    currentState = State.Chase;
                }
                else
                {
                    currentState = State.Move; // กลับไปที่ Move
                }
            }
        }
        else
        {
            currentState = State.Move; // ไม่มีผู้เล่น ให้เดินตามระยะ
        }

        if (animator != null)
        {
            animator.SetBool("isWalking", currentState == State.Move || currentState == State.Chase);
        }
        
    }

    protected override void MoveBehavior()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // เช็คระยะทางกับจุดเริ่มต้นเพื่อเปลี่ยนทิศทาง
        if (Vector2.Distance(startPosition, transform.position) >= patrolDistance)
        {
            Flip(); // เปลี่ยนทิศทาง
            moveDirection = -moveDirection; // กลับทิศทาง
            startPosition = transform.position; // ตั้งค่าจุดเริ่มต้นใหม่
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1; // เปลี่ยนทิศทาง
        transform.localScale = scaler;
    }

    protected override void ChaseBehavior()
    {
        if (player == null) return;

        Vector2 direction = (playerFollow.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, playerFollow.position, chaseSpeed * Time.deltaTime);
    }

    protected override void AttackBehavior()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        FacePlayer(); // เปลี่ยนทิศทางหาผู้เล่น
    }

    private void FacePlayer()
    {
        if (player == null) return;

        bool isPlayerOnRight = playerFollow.position.x > transform.position.x;

        if (isPlayerOnRight && !facingRight)
        {
            Flip();
        }
        else if (!isPlayerOnRight && facingRight)
        {
            Flip();
        }
    }

    public void groundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
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
