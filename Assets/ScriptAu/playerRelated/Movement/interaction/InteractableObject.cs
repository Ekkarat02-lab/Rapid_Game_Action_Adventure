using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : CollidableObject
{
    public bool isInteracted = false;

    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.E) && isCollided == true)
        {
            OnInteracted();
        }
    }

    protected virtual void OnInteracted() 
    {
        if (!isInteracted) 
        {
            isInteracted = true;
            Debug.Log("Interact with: " + this.name);
        }
        
    }
}
