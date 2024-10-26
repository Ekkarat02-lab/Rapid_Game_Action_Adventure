using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
    public int spawnCount;
    public float spawnDelay;
    
     public GameObject enemyToSpawn;
     public Transform[] spawnPoints;

    protected PlayerStats stats;
    protected int currentSpawnIndex = 0;
    protected float spawnTime;
    protected int spawnedEnemy = 0;
    protected bool playerInRange = false;
    protected bool spawnerActive;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        ResetSpawn();
        stats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(spawnedEnemy < spawnCount && playerInRange && spawnerActive)
        {
            spawnTime += Time.deltaTime;

            if (spawnTime >= spawnDelay)
            {
                Transform spawnLocation = spawnPoints[currentSpawnIndex];
                Instantiate(enemyToSpawn, spawnLocation.position, spawnLocation.rotation);
                spawnedEnemy++;
                spawnTime = 0f;
                currentSpawnIndex = (currentSpawnIndex + 1) % spawnPoints.Length;
            } 
  
        }
        if(stats.CurrentHealth <= 0)
        {
            ResetSpawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered");
            playerInRange = true;
            spawnerActive = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exited");
            playerInRange = false;
            ResetSpawn();
        }
    }
    protected virtual void ResetSpawn()
    {
        spawnedEnemy = 0;
        spawnTime = 0f;
        spawnerActive = false;
        Debug.Log("Reset");
    }
}
