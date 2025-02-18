using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawnRoom : EnemySpawnner
{
    private bool isSpawning;
    
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isSpawning)
        {
            isSpawning = true;
        }

        if (isSpawning)
        {
            base.Update(); 
        }
    }

    protected override void ResetSpawn()
    {
        base.ResetSpawn();
        isSpawning = false;
    }

}
