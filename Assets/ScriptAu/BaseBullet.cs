using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public float speed = 50f;
    public Rigidbody2D rb;
    public float lifeTime = 2f;
    private float timer;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= lifeTime)
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
                enemyState.CurrentHealth -= 1;
            }

            if (enemyState.CurrentHealth == 0)
            {
                EnemyState.Instance.Die();
            }
            Destroy(gameObject);
        }
    }

    public void ChangeValue(float value)
    {
        damage = value;

    }
}
