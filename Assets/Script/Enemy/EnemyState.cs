using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public static EnemyState Instance;
    
    public enum State { Idle, Move, Chase, Attack, Dead }
    public State currentState = State.Idle;            

    [Header("Health And Damage")]
    public int Damage = 1;
    
    public float maxHP = 100;
    public float CurrentHealth;
    public float detectionRange = 5f;

    private void Awake()
    {
        Instance = this;
    }
    
    public void Start()
    {
        CurrentHealth = maxHP;
    }

    public void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                IdleBehavior();
                break;
            case State.Move:
                MoveBehavior();
                break;
            case State.Chase:
                ChaseBehavior();
                break;
            case State.Attack:
                AttackBehavior();
                break;
            case State.Dead:
                DeadBehavior();
                break;
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        Debug.Log("Enemy HP: " + CurrentHealth);

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            currentState = State.Dead;
            Die();
        }
    }

    void IdleBehavior()
    {

    }
    
    protected virtual void MoveBehavior()
    {

    }

    protected virtual void ChaseBehavior()
    {
        
    }

    protected virtual void AttackBehavior()
    {
        
    }

    void DeadBehavior()
    {
        Die();
    }
    
    public void Die()
    {
        Destroy(gameObject);
        Debug.Log("Enemy Die");
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}