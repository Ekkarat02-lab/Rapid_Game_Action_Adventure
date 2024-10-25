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
        Uishop.SetActive(false);  // ปิดร้านค้าเริ่มต้น
    }
    protected override void OnInteracted()
    {
        // สลับสถานะการเปิด-ปิดร้านค้า
        isShopOpen = !isShopOpen;
        Uishop.SetActive(isShopOpen);  // เปิดหรือปิดตามสถานะ
        Debug.Log(isShopOpen ? "Shop Opened" : "Shop Closed");
    }
}
