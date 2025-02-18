using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ShopManagerScript : MonoBehaviour
{
    public static ShopManagerScript Instance;
    public PlayerStats playerStats;
    public BaseGun baseGun;
    public int[,] shopItems = new int[5, 5];
    public float coins;
    public TextMeshProUGUI CoinsTXT;
    private void Awake()
    {
        Instance = this;  
    }
    private void Start()
    {
        CoinsTXT.text = "Coins:" + coins.ToString();
        //ID
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;
        //Price
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 50;
        shopItems[2, 3] = 60;
        shopItems[2, 4] = 70;
        //Quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;
    }
    public void AddCoins(float amount)
    {
        coins += amount;
        CoinsTXT.text = "Coins: " + coins.ToString();
        Debug.Log("Added Coins: " + amount + " | Total: " + coins);
    }
    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        ButtonInfo buttonInfo = ButtonRef.GetComponent<ButtonInfo>();
        int upgradeID = buttonInfo.UpgradeID;
        int price = shopItems[2, upgradeID];

        if (coins >= price)
        {
            coins -= price;
            shopItems[3, upgradeID]++;
            CoinsTXT.text = "Coins: " + coins.ToString();
            buttonInfo.QuantityTxt.text = shopItems[3, upgradeID].ToString();

            if (buttonInfo.isHpUpgrade)
            {
                playerStats.ChangeValue(10);
            }
            else if (buttonInfo.isReloadSpeedUpgrade)
            {
                baseGun.ChangeValue(-0.1f);
            }
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }
}
