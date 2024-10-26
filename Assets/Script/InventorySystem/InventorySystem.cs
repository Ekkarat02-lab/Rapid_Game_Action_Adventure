using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public bool[] isFull;
    public ItemData[] Slots;
    public GameObject[] SlotObjects;
    //public GameObject[] InstantiatedItems;
    public Sprite defaultSprite;
    public void useitem(int slotIndex)
    {
        if (Slots[slotIndex] != null)  // ถ้ามีไอเทมในช่องนั้น
        {
            Slots[slotIndex].UseItem();  // ใช้ไอเทม

           

            // เคลียร์ช่องหลังจากใช้ไอเทม
            ClearSlot(slotIndex);
        }


    }
    private void ClearSlot(int slotIndex)
    {
        isFull[slotIndex] = false;  // ทำให้ช่องว่าง
        Slots[slotIndex] = null;  // ล้างข้อมูลไอเทม

        // รีเซ็ตภาพเป็นรูปภาพเริ่มต้น
        Image itemImage = SlotObjects[slotIndex].GetComponentInChildren<Image>();
        if (itemImage != null)
        {
            itemImage.sprite = defaultSprite;
        }
    }
}
