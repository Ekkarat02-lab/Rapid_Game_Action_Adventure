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
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        Destroy(gameObject);
    }
}
