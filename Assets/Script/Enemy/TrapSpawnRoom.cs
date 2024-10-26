using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawnRoom : EnemySpawnner
{
    private bool isSpawning = false;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isSpawning)
        {
            isSpawning = true; // Enable automatic spawning
        }

        // If spawning is active, allow the base class to handle the spawning logic
        if (isSpawning)
        {
            base.Update(); // This will handle the spawning logic (based on delay and other conditions)
        }
    }

    protected override void ResetSpawn()
    {
        base.ResetSpawn();
        isSpawning = false; // Stop spawning when conditions reset (e.g., player leaves range or health is 0)
    }

}
