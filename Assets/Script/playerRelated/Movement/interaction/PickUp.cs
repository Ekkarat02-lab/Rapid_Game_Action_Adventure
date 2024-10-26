using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : InteractableObject
{
    
    private InventorySystem inventory;
    //public GameObject itemObject;
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
        for (int i = 0; i < inventory.SlotObjects.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                inventory.isFull[i] = true;
               
               

                // ���ҧ���� ItemData ������� Inventory
                ItemData newItemData = Instantiate(itemData);
                inventory.Slots[i] = newItemData;  // �红������ Slots

                //Instantiate(itemObject, inventory.SlotObjects[i].transform, false);
                Image itemImage = inventory.SlotObjects[i].GetComponentInChildren<Image>();
                                
                if (itemImage != null)
                {
                    itemImage.sprite = itemData.main; // ��˹� Sprite �ҡ ItemData
                }
                

                Destroy(gameObject);
                break;
            }
        }
    }
}
