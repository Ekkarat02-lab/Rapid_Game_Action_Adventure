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
    public Sprite defaultSprite;

    public ParticleSystem useItemEffect;  // Particle System สำหรับเอฟเฟกต์การใช้ไอเท็ม
    public Transform playerTransform;  // ตำแหน่งของผู้เล่น

    public void useitem(int slotIndex)
    {
        if (Slots[slotIndex] != null)  
        {
            Slots[slotIndex].UseItem();  // เรียกใช้ไอเท็มในสล็อตที่กำหนด

            ShowUseItemEffect();  // แสดงเอฟเฟกต์ที่ตำแหน่งของผู้เล่น
            ClearSlot(slotIndex);  // ลบไอเท็มออกจากสล็อต
        }
    }

    private void ClearSlot(int slotIndex)
    {
        isFull[slotIndex] = false;  
        Slots[slotIndex] = null;  

        Image itemImage = SlotObjects[slotIndex].GetComponentInChildren<Image>();
        if (itemImage != null)
        {
            itemImage.sprite = defaultSprite;
        }
    }

    private void ShowUseItemEffect()
    {
        if (useItemEffect != null && playerTransform != null)
        {
            // สร้างเอฟเฟกต์ Particle ที่ตำแหน่งของผู้เล่น
            ParticleSystem effect = Instantiate(useItemEffect, playerTransform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration);  // ลบเอฟเฟกต์หลังจากเล่นเสร็จ
        }
        else
        {
            Debug.LogWarning("ไม่มี Particle System หรือไม่มีการตั้งค่าตำแหน่งผู้เล่น!");
        }
    }
}