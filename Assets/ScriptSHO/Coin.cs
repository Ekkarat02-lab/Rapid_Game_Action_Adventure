using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float coinValue         ;  // ��Ť�Ңͧ����­���

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))  // ��Ǩ�ͺ����繼������������
        {
            ShopManagerScript.Instance.AddCoins(coinValue);  // ��������­
            Destroy(gameObject);  // ���������­��ѧ��
        }
    }
}
