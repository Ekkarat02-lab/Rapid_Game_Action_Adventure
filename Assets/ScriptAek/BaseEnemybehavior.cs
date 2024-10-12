using UnityEngine;

public class BaseEnemyBehavior : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public float detectionRange = 5f; 

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log("Enemy HP: " + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }
    
    public void Die()
    {
        Destroy(gameObject);
        Debug.Log("Enemy ตายแล้ว");
    }

    void Start()
    {
        currentHP = maxHP;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}