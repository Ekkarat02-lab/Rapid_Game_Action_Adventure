using UnityEngine;

public class EnemyBehavior : BaseEnemyBehavior
{
    public Transform player;  // ผู้เล่น
    public float moveSpeed = 2f;  // ความเร็วในการเคลื่อนที่ของศัตรู
    public float chaseSpeed = 4f;  // ความเร็วในการไล่ตามผู้เล่น
    public Vector2 moveDirection = Vector2.right;  // ทิศทางเริ่มต้นในการเดิน
    public float patrolDistance = 5f;  // ระยะการเดินไป-กลับ
    private Vector2 startPosition;

    public float directionAttack = 1.5f;  // ระยะโจมตี
    public float jumpForce = 5f;  // ความแรงในการกระโดด
    private Rigidbody2D rb;  // Reference to the Rigidbody2D component

    public Transform rayPointG;
    public float rayDistanceG;
    private int groundLayerIndex;
    protected bool isGrounded;
    
    private void Start()
    {
        startPosition = transform.position;
        currentState = EnemyState.Move;
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component
        groundLayerIndex = LayerMask.NameToLayer("groundLayer");
    }

    private void Update()
    {
        base.Update();  // เรียกการทำงานของ base class เพื่อจัดการ state
        groundCheck();
            
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= directionAttack)
        {
            currentState = EnemyState.Attack;  // เปลี่ยนสถานะเป็น Attack เมื่อผู้เล่นอยู่ในระยะโจมตี
        }
        else if (distanceToPlayer <= detectionRange)
        {
            currentState = EnemyState.Chase;  // เปลี่ยนสถานะเป็น Chase เมื่อผู้เล่นอยู่ในระยะ
        }
        else if (currentState == EnemyState.Chase && distanceToPlayer > detectionRange)
        {
            currentState = EnemyState.Move;  // เปลี่ยนกลับเป็น Move เมื่อผู้เล่นออกจากระยะ
        }
    }

    protected override void MoveBehavior()
    {
        // พฤติกรรมการเดินไป-กลับในระยะที่กำหนด
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // ตรวจสอบระยะการเดินจากจุดเริ่มต้น
        if (Vector2.Distance(startPosition, transform.position) >= patrolDistance)
        {
            moveDirection = -moveDirection;  // เปลี่ยนทิศทางเมื่อเดินถึงขอบเขต
        }
    }

    protected override void ChaseBehavior()
    {
        // ไล่ตามผู้เล่นเมื่ออยู่ในสถานะ Chase
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && currentState == EnemyState.Attack)
        {
            // ทำลายผู้เล่นเมื่อถูกกระโดดใส่
            Destroy(collision.gameObject);  // ทำให้ผู้เล่นหายไป
            Debug.Log("Player destroyed!");
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            // คำนวณทิศทางการสะท้อน
            Vector2 reflectDirection = (transform.position - collision.transform.position).normalized;
            transform.Translate(reflectDirection * 1);  // สะท้อนจากทิศทางที่โดนกระสุน
        }
    }

    protected override void AttackBehavior()
    {
        // เมื่ออยู่ในสถานะ Attack ให้กระโดดไปหาผู้เล่น
        JumpTowardsPlayer();
    }

    private void JumpTowardsPlayer()
    {
        if (isGrounded)
        {
            // คำนวณทิศทางการกระโดด
            Vector2 jumpDirection = (player.position - transform.position).normalized;
            // ใช้ AddForce เพื่อกระโดด
            rb.velocity = new Vector2(jumpDirection.x * moveSpeed, jumpForce);  // กำหนดแรงกระโดด
            Debug.Log("Jumping towards Player!");
            isGrounded = false;
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
                //animator.SetBool("IsJumping", false);
            }
            else
            {
                isGrounded = false;
                //animator.SetBool("IsJumping", true);
            }
        }
        else
        {
            isGrounded = false;
            //animator.SetBool("IsJumping", true);
        }
        Debug.DrawRay(rayPointG.position, Vector2.down * rayDistanceG, Color.red);
    }
}
