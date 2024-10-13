using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : UnitStats
{

    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CurrentHealth = maxHealth;
        HealthBar.instance.SetHealth(maxHealth);
    }
    private void Update()
    {
        HealthBar.instance.SetHealth(CurrentHealth);
    }
    
    public void ChangeValue (float value)
    {
        maxHealth = value;
    }
    // Coroutine ที่ทำร้ายผู้เล่น 1 หน่วยต่อวินาที

}
