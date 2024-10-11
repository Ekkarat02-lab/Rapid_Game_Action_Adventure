using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;        // ผู้เล่น
    public float detectionRange = 5f; // ระยะที่ศัตรูจะเริ่มไล่ตาม
    public float moveSpeed = 2f;    // ความเร็วในการเคลื่อนที่ของศัตรู
    public int enemyHP = 100;       // HP ของศัตรู

    private void Update()
    {
        // ตรวจสอบว่าผู้เล่นอยู่ในระยะที่กำหนด
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            // เคลื่อนที่เข้าหาผู้เล่น
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        // คำนวณทิศทางจากศัตรูไปยังผู้เล่น
        Vector2 direction = (player.position - transform.position).normalized;

        // เคลื่อนที่เข้าหาผู้เล่น
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ตรวจสอบการชนว่าเป็นผู้เล่นและผู้เล่นมี tag เป็น "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // ลบผู้เล่นออก
            Destroy(collision.gameObject);

            // เพิ่ม HP ให้ศัตรู
            IncreaseHP(20);  // เพิ่ม 20 HP (สามารถปรับค่าได้ตามต้องการ)
        }
    }


    void IncreaseHP(int amount)
    {
        enemyHP += amount;
        Debug.Log("Enemy HP: " + enemyHP);
    }

    // วาดเส้นแสดงระยะการตรวจจับใน Scene View
    private void OnDrawGizmosSelected()
    {
        // กำหนดสีสำหรับ Gizmos (สามารถเปลี่ยนสีได้ตามต้องการ)
        Gizmos.color = Color.red;

        // วาดวงกลมเพื่อแสดงระยะ detectionRange
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}