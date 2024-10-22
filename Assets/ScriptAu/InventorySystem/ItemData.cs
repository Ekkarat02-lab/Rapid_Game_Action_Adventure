using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    [SerializeField] private string ItemName;
    [SerializeField] private string ItemType;
    public Sprite itemSprite;
    public Sprite main;
    [SerializeField] private string ItemDescription;
    private PlayerStats playerStats;
    public bool isHealingItem; // เพิ่มตัวแปรเพื่อตรวจสอบว่าเป็นไอเทมเพิ่มเลือดหรือไม่
    public void UseItem() // เปลี่ยนชื่อฟังก์ชันให้เป็น UseItem เพื่อความชัดเจน
    {
        if (isHealingItem)
        {
            playerStats.CurrentHealth = playerStats.maxHealth;
            
            // เพิ่มโค้ดสำหรับแสดงผลการเพิ่มเลือด เช่น แสดงข้อความ หรือ อนิเมชั่น
        }
    }
}
