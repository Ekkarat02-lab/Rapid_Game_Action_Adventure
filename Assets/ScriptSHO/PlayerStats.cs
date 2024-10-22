using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : UnitStats
{
    public static PlayerStats Instance;

    public int Damage = 1;
    
    public GameObject gameOverUI;
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
            // เพิ่ม Damage ของศัตรู 1 หน่วยเมื่อผู้เล่นตาย
            if (EnemyState.Instance != null)
            {
                EnemyState.Instance.Damage += Damage;
                Debug.Log("Enemy Damage Increased: " + EnemyState.Instance.Damage);
            }
            Time.timeScale = 0f;
            gameOverUI.SetActive(true);
           
        }
    }
    
    public void ChangeValue (float value)
    {
        maxHealth = value;
    }
    // Coroutine ที่ทำร้ายผู้เล่น 1 หน่วยต่อวินาที

}
