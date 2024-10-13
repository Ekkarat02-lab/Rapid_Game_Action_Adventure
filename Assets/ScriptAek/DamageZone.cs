using System.Collections;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damagePerSecond = 1; // ความเสียหายต่อวินาที
    private bool isPlayerInZone = false; // สถานะว่าผู้เล่นอยู่ในโซนหรือไม่

    private Material playerMaterial; // เก็บ Material ของผู้เล่น
    private Color originalColor; // สีดั้งเดิมเมื่อผู้เล่นมี HP สูงสุด
    private Color lowHpColor = Color.green; // สีเมื่อ HP ต่ำ

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่าผู้เล่นเข้าสู่โซนหรือไม่
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true; // ผู้เล่นอยู่ในโซน
            PlayerStats player = other.GetComponent<PlayerStats>(); // ดึงข้อมูลผู้เล่น
            playerMaterial = player.GetComponent<SpriteRenderer>().material; // ดึง Material จาก SpriteRenderer ของผู้เล่น
            originalColor = playerMaterial.color; // เก็บสีดั้งเดิมของผู้เล่น
            StartCoroutine(DamagePlayer(player)); // เริ่มทำร้ายผู้เล่น
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // เมื่อผู้เล่นออกจากโซน
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false; // ผู้เล่นออกจากโซน
            StopAllCoroutines(); // หยุดการทำร้ายผู้เล่น
            if (playerMaterial != null)
            {
                playerMaterial.color = originalColor; // คืนค่าสีเดิมของผู้เล่นเมื่อออกจากโซน
            }
        }
    }

    // Coroutine ที่ทำร้ายผู้เล่น 1 หน่วยต่อวินาที
    private IEnumerator DamagePlayer(PlayerStats player)
    {
        while (isPlayerInZone && player != null)
        {
            // ลด HP ของผู้เล่น
            player.CurrentHealth -= damagePerSecond;

            // คำนวณอัตราส่วนของ HP ที่เหลืออยู่
            float healthPercent = (float)player.CurrentHealth / player.maxHealth;

            // ใช้ Lerp เปลี่ยนสี Material ของผู้เล่นให้ค่อย ๆ เปลี่ยนตาม HP
            if (playerMaterial != null)
            {
                playerMaterial.color = Color.Lerp(lowHpColor, originalColor, healthPercent);
            }

            // เช็คว่า HP ของผู้เล่นเหลือ 0 หรือไม่
            if (player.CurrentHealth <= 0)
            {
                player.CurrentHealth = 0;
                // เรียกใช้ฟังก์ชัน game over หรือ logic อื่นๆ ที่ต้องการ
            }

            // รอ 1 วินาทีก่อนลด HP อีกครั้ง
            yield return new WaitForSeconds(1f);
        }
    }
}
