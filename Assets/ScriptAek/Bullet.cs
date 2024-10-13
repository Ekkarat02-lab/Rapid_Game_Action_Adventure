using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;

    private Vector2 targetDirection;

    public void Initialize(Vector2 direction)
    {
        targetDirection = direction;
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(targetDirection * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.CurrentHealth -= 1;
            }

            if (playerStats.CurrentHealth == 0)
            {
                Destroy(collision.gameObject);
            }

            Destroy(gameObject);
        }
    }
}