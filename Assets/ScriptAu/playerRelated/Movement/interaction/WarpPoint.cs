using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPoint : InteractableObject
{
    private bool uiOpened = false;
    [SerializeField] private GameObject warpUi;
    
    protected override void Start()
    {
        base.Start();
        warpUi.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.E) && isCollided == true)
        {
            OnInteracted();
            uiOpened = !uiOpened;
        }
    }
    protected override void OnInteracted()
    {
        warpUi.SetActive(uiOpened);
    }

}
