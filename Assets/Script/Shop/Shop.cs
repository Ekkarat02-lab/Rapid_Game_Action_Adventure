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
        Uishop.SetActive(false);
    }
    protected override void OnInteracted()
    {

        isShopOpen = !isShopOpen;
        Uishop.SetActive(isShopOpen);  
        Debug.Log(isShopOpen ? "Shop Opened" : "Shop Closed");
    }
}
