using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float coinValue         ;  // มูลค่าของเหรียญนี้

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))  // ตรวจสอบว่าเป็นผู้เล่นหรือไม่
        {
            ShopManagerScript.Instance.AddCoins(coinValue);  // เพิ่มเหรียญ
            Destroy(gameObject);  // ทำลายเหรียญหลังเก็บ
        }
    }
}
