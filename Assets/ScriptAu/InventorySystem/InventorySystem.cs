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
        if (Slots[slotIndex] != null)  // ���������㹪�ͧ���
        {
            Slots[slotIndex].UseItem();  // ������

           

            // �������ͧ��ѧ�ҡ������
            ClearSlot(slotIndex);
        }


    }
    private void ClearSlot(int slotIndex)
    {
        isFull[slotIndex] = false;  // ������ͧ��ҧ
        Slots[slotIndex] = null;  // ��ҧ����������

        // �����Ҿ���ٻ�Ҿ�������
        Image itemImage = SlotObjects[slotIndex].GetComponentInChildren<Image>();
        if (itemImage != null)
        {
            itemImage.sprite = defaultSprite;
        }
    }
}
