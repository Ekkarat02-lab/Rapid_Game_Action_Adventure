using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public float maxHealth;
    public float CurrentHealth;
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
}
