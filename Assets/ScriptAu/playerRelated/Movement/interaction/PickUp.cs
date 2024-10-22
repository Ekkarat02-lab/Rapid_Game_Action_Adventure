using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : InteractableObject
{
    
    private InventorySystem inventory;
    public GameObject itemObject;
    public ItemData itemData;
    protected override void Start()
    {
        base.Start();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
    }
    protected override void OnInteracted()
    {
        if(!isInteracted)
        {
            PickUpItem();
        }
    }
    private void PickUpItem()
    {
        isInteracted = true;
        for (int i = 0; i < inventory.Slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                inventory.isFull[i] = true;
                Instantiate(itemObject, inventory.Slots[i].transform, false);
                /*Image itemImage = inventory.Slots[i].GetComponentInChildren<Image>();
                if (itemImage != null)
                {
                    itemImage.sprite = itemData.itemSprite; // กำหนด Sprite จาก ItemData
                }*/

                Destroy(gameObject);
                break;
            }
        }
    }
}
