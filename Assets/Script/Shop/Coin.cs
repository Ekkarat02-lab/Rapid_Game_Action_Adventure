using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float coinValue; 

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShopManagerScript.Instance.AddCoins(coinValue);
            Destroy(gameObject);
        }
    }
}
