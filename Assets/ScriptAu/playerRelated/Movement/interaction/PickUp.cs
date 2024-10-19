using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : InteractableObject
{
    
    private InventorySystem inventory;
    public GameObject itemObject;

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
                Destroy(gameObject);
                break;
            }
        }
    }
}
