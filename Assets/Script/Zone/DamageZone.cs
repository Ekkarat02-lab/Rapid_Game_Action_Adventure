using System.Collections;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float damagePerSecond = 1;
    private bool isPlayerInZone = false;
    public GameObject gameOverUI;
    private Material playerMaterial;
    private Color originalColor;
    private Color lowHpColor = Color.green; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
            PlayerStats player = other.GetComponent<PlayerStats>();
            playerMaterial = player.GetComponent<SpriteRenderer>().material;
            originalColor = playerMaterial.color;
            StartCoroutine(DamagePlayer(player));
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
            StopAllCoroutines();
            if (playerMaterial != null)
            {
                playerMaterial.color = originalColor;
            }
        }
    }

    private IEnumerator DamagePlayer(PlayerStats player)
    {
        while (isPlayerInZone && player != null)
        {
            if (player.CurrentHealth > 0) 
            { 
                player.CurrentHealth -= damagePerSecond;
            }

            float healthPercent = (float)player.CurrentHealth / player.maxHealth;

            if (playerMaterial != null)
            {
                playerMaterial.color = Color.Lerp(lowHpColor, originalColor, healthPercent);
            }

            if (player.CurrentHealth <= 0)
            {
                player.CurrentHealth = 0;
                Time.timeScale = 0f;
                gameOverUI.SetActive(true);
                
            }
            Debug.Log("Player's current health: " + player.CurrentHealth);
            yield return new WaitForSeconds(2.15f);
            
        }
    }
}
