using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPoint : InteractableObject
{
    
    private bool uiOpened = false;
    private bool isPlayerNearby;
    public bool[] canWarpTo;
    public int warpIndex;
    [SerializeField] private GameObject warpUi;
    
    
    
    protected override void Start()
    {
        Instance = this;
        base.Start();
        warpUi.SetActive(false);
        canWarpTo = new bool[5];
        canWarpTo[warpIndex] = false;
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNearby == true)
        {
            uiOpened = !uiOpened;
            OnInteracted();
        }
    }
    protected override void OnInteracted()
    {
        warpUi.SetActive(uiOpened);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            canWarpTo[warpIndex] = true;
            Debug.Log("Warp unlocked: " + canWarpTo[warpIndex]);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            warpUi.SetActive(false);
            isInteracted = false;
        }
    }

}
