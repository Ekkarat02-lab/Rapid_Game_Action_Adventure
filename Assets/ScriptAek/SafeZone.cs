using System.Collections;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public int healPerSecond = 10;
    private bool isPlayerInZone = false; 

    private Material playerMaterial;
    private Color healingColor = Color.white;
    private Color originalColor; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
            PlayerStats player = other.GetComponent<PlayerStats>();
            playerMaterial = player.GetComponent<SpriteRenderer>().material;
            originalColor = playerMaterial.color;
            StartCoroutine(HealPlayer(player));
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

    private IEnumerator HealPlayer(PlayerStats player)
    {
        while (isPlayerInZone && player != null)
        {
            player.CurrentHealth += healPerSecond;

            if (player.CurrentHealth > player.maxHealth)
            {
                player.CurrentHealth = player.maxHealth;
            }

            if (playerMaterial != null)
            {
                playerMaterial.color = healingColor;
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
