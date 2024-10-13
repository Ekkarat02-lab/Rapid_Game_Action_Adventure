using System.Collections;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public int healPerSecond = 10; // ปริมาณการฟื้นฟู HP ต่อวินาที
    private bool isPlayerInZone = false; // สถานะว่าผู้เล่นอยู่ใน Safe Zone หรือไม่

    private Material playerMaterial; // เก็บ Material ของผู้เล่น
    private Color healingColor = Color.white; // สีเมื่อผู้เล่นฟื้นฟู HP
    private Color originalColor; // สีดั้งเดิมของผู้เล่น

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่าแท็ก Player เข้ามาใน SafeZone หรือไม่
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true; // ผู้เล่นอยู่ใน SafeZone
            PlayerStats player = other.GetComponent<PlayerStats>(); // ดึงข้อมูลผู้เล่น
            playerMaterial = player.GetComponent<SpriteRenderer>().material; // ดึง Material จาก SpriteRenderer ของผู้เล่น
            originalColor = playerMaterial.color; // เก็บสีดั้งเดิมของผู้เล่น
            StartCoroutine(HealPlayer(player)); // เริ่มฟื้นฟู HP ของผู้เล่น
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // เมื่อผู้เล่นออกจาก SafeZone
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false; // ผู้เล่นออกจาก SafeZone
            StopAllCoroutines(); // หยุดการฟื้นฟู HP
            if (playerMaterial != null)
            {
                playerMaterial.color = originalColor; // คืนค่าสีเดิมของผู้เล่นเมื่อออกจาก SafeZone
            }
        }
    }

    // Coroutine ที่ฟื้นฟู HP ของผู้เล่น 10 หน่วยต่อวินาที
    private IEnumerator HealPlayer(PlayerStats player)
    {
        while (isPlayerInZone && player != null)
        {
            // เพิ่ม HP ของผู้เล่น
            player.CurrentHealth += healPerSecond;

            // ตรวจสอบไม่ให้ HP เกิน maxHP
            if (player.CurrentHealth > player.maxHealth)
            {
                player.CurrentHealth = player.maxHealth;
            }

            // ถ้ากำลังเพิ่ม HP ให้เปลี่ยนสีเป็นสีขาว
            if (playerMaterial != null)
            {
                playerMaterial.color = healingColor;
            }

            // รอ 1 วินาทีก่อนเพิ่ม HP อีกครั้ง
            yield return new WaitForSeconds(1f);
        }
    }
}
