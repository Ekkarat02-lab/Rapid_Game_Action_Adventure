using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    [SerializeField] private string ItemName;
    [SerializeField] private string ItemType;
    public Sprite itemSprite;
    public Sprite main;
    [SerializeField] private string ItemDescription;
    private PlayerStats playerStats;
    public bool isHealingItem; // ������������͵�Ǩ�ͺ����������������ʹ�������
    public void UseItem() // ����¹���Ϳѧ��ѹ����� UseItem ���ͤ����Ѵਹ
    {
        if (isHealingItem)
        {
            playerStats.CurrentHealth = playerStats.maxHealth;
            
            // ����������Ѻ�ʴ��š���������ʹ �� �ʴ���ͤ��� ���� ͹������
        }
    }
}
