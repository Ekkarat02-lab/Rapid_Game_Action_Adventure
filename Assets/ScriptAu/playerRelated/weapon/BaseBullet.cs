using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public float speed = 50f;
    public Rigidbody2D rb;
    public float lifeTime = 2f;
    private float timer;
    public float damage = 10f;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            EnemyState enemyState = hitInfo.gameObject.GetComponent<EnemyState>();
            if (enemyState != null)
            {
                enemyState.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    public void ChangeValue(float value)
    {
        damage = value; 
    }
}