using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : UnitStats
{
    public static PlayerStats Instance;

    public int TakeDamageEnemy = 1;
    
    public Transform spawnPoint;

    //public GameObject gameOverUI;
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
                EnemyState.Instance.Damage += TakeDamageEnemy;
                Debug.Log("Enemy Damage Increased: " + EnemyState.Instance.Damage);
            }

            // ย้ายผู้เล่นกลับไปที่จุด Spawn
            RespawnPlayer();

            /* Time.timeScale = 0f;
            gameOverUI.SetActive(true); */
        }
    }

    public void RespawnPlayer()
    {
        // ย้าย Player กลับไปที่ตำแหน่ง Spawn
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
            Debug.Log("Player respawned at spawn point.");
            
            // คืนค่า HP ของ Player เต็มหลัง respawn
            CurrentHealth = maxHealth;
            HealthBar.instance.SetMaxHealth(maxHealth);
            HealthBar.instance.SetHealth(CurrentHealth);
        }
        else
        {
            Debug.LogWarning("Spawn point not set!");
        }
    }

    public void ChangeValue(float value)
    {
        maxHealth = value;
    }
}