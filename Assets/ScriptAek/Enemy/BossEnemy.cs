using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyState
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;

    private GameObject player;
    private float nextFireTime;

    [Header("Player Detection and Movement")]
    public float moveSpeed = 2f; 
    public float chaseSpeed = 4f;
    public Vector2 moveDirection = Vector2.right;
    public float patrolDistance = 5f;
    private Vector2 startPosition;
    private Transform Player;
    public float DetectionAttack = 1.5f;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        CurrentHealth = maxHP;
    }

    private void Update()
    {
        if (player != null && Vector2.Distance(transform.position, player.transform.position) <= detectionRange)
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    protected override void MoveBehavior()
    {
        // Basic movement logic
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Patrol logic
        if (Vector2.Distance(startPosition, transform.position) >= patrolDistance)
        {
            moveDirection = -moveDirection; // Reverse direction when patrol distance is reached
            startPosition = transform.position; // Update the start position after reversing
        }
    }
    
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Vector2 direction = (player.transform.position - firePoint.position).normalized;
        bullet.GetComponent<Bullet>().Initialize(direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            HandleDirectionChange(collision);
        }
    }
    
    private void HandleDirectionChange(Collision2D collision)
    {
        // Check if the enemy hits something on the left or right
        if (collision.contacts[0].normal.x > 0) // Hit from the left side
        {
            moveDirection = Vector2.right; // Move right
        }
        else if (collision.contacts[0].normal.x < 0) // Hit from the right side
        {
            moveDirection = Vector2.left; // Move left
        }
    }
    protected override void ChaseBehavior()
    {
        // Return early if player is null
        if (player == null)
        {
            return;
        }

        Vector2 direction = (Player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, Player.position, chaseSpeed * Time.deltaTime);
    }
}