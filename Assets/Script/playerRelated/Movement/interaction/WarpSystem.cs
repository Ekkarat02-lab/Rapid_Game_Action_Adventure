using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarpSystem : MonoBehaviour
{
    public Transform[] destination;
    public GameObject player;
    private int warpTo = 0;
    [SerializeField] private GameObject warpUi;

    // Call this method from UI button
    public void WarpToDestination(int index)
    {
        if (index >= 0 && index < destination.Length) // Check if index is valid
        {
            warpTo = index;
            player.transform.position = destination[warpTo].position; // Teleport player to destination
            Debug.Log("Teleported to: " + destination[warpTo].name); // Optional log
        }
        else
        {
            Debug.LogError("Invalid destination index!");
        }
    }
    public Transform GetDestination() 
    {
        return destination[warpTo];
    }
    
    public void CloseWarp()
    {
        if (warpUi != null)
        {
            warpUi.SetActive(false); // Deactivate the Warp UI
            Debug.Log("Warp UI closed.");
        }
        else
        {
            Debug.LogWarning("Warp UI reference is missing!");
        }
    }
}
