using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitBoxBoss : MonoBehaviour
{
    public int damage; // กำหนดค่าความเสียหาย

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = hitInfo.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}