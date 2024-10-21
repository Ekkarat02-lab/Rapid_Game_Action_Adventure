using UnityEngine;

public class ZoneEnemy : MonoBehaviour
{
    public float takeDamage = 10f;  // ความเสียหายที่ต้องการให้เกิด
    public int bonusMaxHP = 20;  // จำนวน maxHP ที่จะเพิ่มให้กับศัตรู

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่าวัตถุที่ชนมี Tag เป็น "Enemy" หรือไม่
        if (other.CompareTag("Enemy"))
        {
            // เข้าถึงสคริปต์ EnemyState ของวัตถุที่เข้ามา
            EnemyState enemy = other.GetComponent<EnemyState>();
            if (enemy != null)
            {
                // เพิ่ม maxHP ของศัตรู
                enemy.maxHP += bonusMaxHP;

                // ตรวจสอบให้แน่ใจว่า CurrentHealth ไม่เกินค่า maxHP ใหม่
                enemy.CurrentHealth = Mathf.Min(enemy.CurrentHealth + bonusMaxHP, enemy.maxHP);

                // เรียกใช้ฟังก์ชัน TakeDamage ของ EnemyState และส่งค่าความเสียหาย
                enemy.TakeDamage(takeDamage);
                Debug.Log("Enemy maxHP increased by: " + bonusMaxHP);
                Debug.Log("Enemy took damage: " + takeDamage);
            }
        }
    }
}