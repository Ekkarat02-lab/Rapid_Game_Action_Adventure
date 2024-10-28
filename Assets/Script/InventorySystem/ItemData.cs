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
   
    public bool isHealingItem;
    public virtual void UseItem()
    {
        if (isHealingItem)
        {
            Heal();
        }
    }
    public void Heal()
    {
        playerStats.CurrentHealth = playerStats.maxHealth;
    }
}
