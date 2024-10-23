using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    public Sprite main;
    public PlayerStats playerStats;
    [SerializeField] private string ItemName;
    [SerializeField] private string ItemType;
    [SerializeField] private string ItemDescription;
   
    public bool isHealingItem; // เพิ่มตัวแปรเพื่อตรวจสอบว่าเป็นไอเทมเพิ่มเลือดหรือไม่
    public virtual void UseItem() // เปลี่ยนชื่อฟังก์ชันให้เป็น UseItem เพื่อความชัดเจน
    {
        if (isHealingItem)
        {
            Heal();
            

        }
        Debug.Log($"{ItemName} ถูกใช้แล้ว");
    }
    public void Heal()
    {
        playerStats.CurrentHealth = playerStats.maxHealth;
    }

}
