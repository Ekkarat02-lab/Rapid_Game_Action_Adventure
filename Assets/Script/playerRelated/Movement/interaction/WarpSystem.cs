using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarpSystem : MonoBehaviour
{
    [SerializeField]private WarpPoint[] warp;
    public Transform[] destination;
    public GameObject player;
    private int warpTo = 0;
    [SerializeField] private GameObject warpUi;


    private void Start()
    {
        if (warp == null) // Check if `warp` is already assigned
        {
            warp = GetComponent<WarpPoint[]>();
        }

        if (warp == null) // If it's still null, log an error
        {
            Debug.LogError("WarpPoint reference is missing and could not be found on the GameObject!");
        }
        else
        {
            warp[warpTo].warpIndex = 0; // Set default or relevant index
        }
    }
    // Call this method from UI button
    public void WarpToDestination(int index)
    {
        if (warp == null)
        {
            Debug.LogError("WarpPoint reference is missing!");
            return;
        }

        if (warp[index].canWarpTo == null || index >= warp[index].canWarpTo.Length)
        {
            Debug.LogError("Warp destinations not initialized properly.");
            return;
        }

        if (warp[index].canWarpTo[index] == true)
        {
            if (destination == null || index >= destination.Length || destination[index] == null)
            {
                Debug.LogError("Invalid destination or index in WarpSystem.");
                return;
            }

            player.transform.position = destination[index].position; // Teleport player to destination
            Debug.Log("Teleported to: " + destination[index].name); // Optional log
        }
        else
        {
            Debug.Log("Warp isn't unlocked for destination " + index);
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
