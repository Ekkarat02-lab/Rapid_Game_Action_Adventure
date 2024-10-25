using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : InteractableObject
{
    [SerializeField] private GameObject Uishop;
    private bool isShopOpen = false;
    
    protected override void Start()
    {
        base.Start();
        Uishop.SetActive(false);  // �Դ��ҹ����������
    }
    protected override void OnInteracted()
    {
        // ��ѺʶҹС���Դ-�Դ��ҹ���
        isShopOpen = !isShopOpen;
        Uishop.SetActive(isShopOpen);  // �Դ���ͻԴ���ʶҹ�
        Debug.Log(isShopOpen ? "Shop Opened" : "Shop Closed");
    }
}
