using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // ความเร็วการเคลื่อนที่ของผู้เล่น
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // เข้าถึง Rigidbody2D ของผู้เล่น
    }

    void Update()
    {
        // รับอินพุตจากปุ่ม A และ D (แนวนอน)
        float moveX = Input.GetAxisRaw("Horizontal"); 
        movement = new Vector2(moveX, 0);
    }

    void FixedUpdate()
    {
        // เคลื่อนที่ผู้เล่นโดยการใช้ Rigidbody2D
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }
}