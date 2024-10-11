using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint;
    public float detectionRange = 5f;
    public float fireRate = 1f; 
    public int maxHP = 100;

    private int currentHP;
    private GameObject player; 
    private float nextFireTime; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentHP = maxHP;
    }

    void Update()
    {
        if (player != null && Vector2.Distance(transform.position, player.transform.position) < detectionRange)
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

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die(); 
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}