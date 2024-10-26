using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonInfo : MonoBehaviour
{
    public int UpgradeID;
    public TextMeshProUGUI PirceTxt;
    public TextMeshProUGUI QuantityTxt;
    public GameObject ShopManager;
    public bool isHpUpgrade;  
    public bool isReloadSpeedUpgrade;
    void Update()
    {
        PirceTxt.text = "Price:" + ShopManager.GetComponent<ShopManagerScript>().shopItems[2,UpgradeID].ToString();
        QuantityTxt.text = ShopManager.GetComponent<ShopManagerScript>().shopItems[3, UpgradeID].ToString();
    }
}
