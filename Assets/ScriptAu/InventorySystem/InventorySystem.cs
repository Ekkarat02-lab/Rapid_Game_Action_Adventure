using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public bool[] isFull;
    public ItemData[] Slots;
    public void useitem(int E)
    {
        Slots[E].UseItem();
    }

}
