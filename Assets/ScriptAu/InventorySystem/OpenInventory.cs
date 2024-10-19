using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUi;
    private bool openInventory;
    // Start is called before the first frame update
    void Start()
    {
        inventoryUi.SetActive(false);
        openInventory = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            openInventory = !openInventory;
            inventoryUi.SetActive(openInventory);
        }
        
    }
}
