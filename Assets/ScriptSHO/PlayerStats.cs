using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : UnitStats
{
    public static PlayerStats Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CurrentHealth = maxHealth;
        HealthBar.instance.SetMaxHealth(maxHealth);
    }
    private void FixedUpdate()
    {
        HealthBar.instance.SetHealth(CurrentHealth);
    }
    
    public void TakeDamage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        Debug.Log("Player HP: " + CurrentHealth);
        if (CurrentHealth <= 0 || CurrentHealth == 0)
        {
            Destroy(gameObject);
        }
        // สามารถเพิ่มเอฟเฟกต์เสียง, การอัปเดต UI หรือการตอบสนองอื่นๆ เมื่อ HP ลดได้ที่นี่
    }
    
    public void ChangeValue (float value)
    {
        maxHealth = value;
    }
    // Coroutine ที่ทำร้ายผู้เล่น 1 หน่วยต่อวินาที

}
