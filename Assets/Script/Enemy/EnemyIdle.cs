using UnityEngine;

public class Enemy : EnemyState
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;

    private GameObject player;
    private float nextFireTime;

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

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Vector2 direction = (player.transform.position - firePoint.position).normalized;
        bullet.GetComponent<Bullet>().Initialize(direction);
    }
}