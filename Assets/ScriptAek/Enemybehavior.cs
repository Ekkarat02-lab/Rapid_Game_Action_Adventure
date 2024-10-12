using UnityEngine;

public class EnemyBehavior : BaseEnemyBehavior
{
    public Transform player;
    public float moveSpeed = 2f; 

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}