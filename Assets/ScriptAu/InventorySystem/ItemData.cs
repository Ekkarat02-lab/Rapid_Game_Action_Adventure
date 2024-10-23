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
   
    public bool isHealingItem; // ������������͵�Ǩ�ͺ����������������ʹ�������
    public virtual void UseItem() // ����¹���Ϳѧ��ѹ����� UseItem ���ͤ����Ѵਹ
    {
        if (isHealingItem)
        {
            Heal();
            

        }
        Debug.Log($"{ItemName} �١������");
    }
    public void Heal()
    {
        playerStats.CurrentHealth = playerStats.maxHealth;
    }

}
