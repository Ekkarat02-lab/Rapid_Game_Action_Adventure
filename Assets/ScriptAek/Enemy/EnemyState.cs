using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public static EnemyState Instance;
    
    public enum State { Idle, Move, Chase, Attack, Dead }
    public State currentState = State.Idle;            

    [Header("Health And Damage")]
    public int Damage = 1;
    
    public int maxHP = 100;
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
        // Behavior updates based on the current state
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
            currentState = State.Dead;  // State Dead
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        Debug.Log("Enemy Die");
    }

    // ฟังก์ชันพฤติกรรมขณะ Idle
    void IdleBehavior()
    {
        // สามารถใส่พฤติกรรมเช่นหยุดหรือรอที่นี่ได้
    }

    // ฟังก์ชันพฤติกรรมขณะเดิน
    protected virtual void MoveBehavior()
    {
        // พฤติกรรมพื้นฐานสำหรับการเดิน (สามารถ override ใน EnemyBehavior)
    }

    // ฟังก์ชันพฤติกรรมขณะไล่ตามผู้เล่น
    protected virtual void ChaseBehavior()
    {
        // พฤติกรรมพื้นฐานสำหรับการไล่ตามผู้เล่น (สามารถ override ใน EnemyBehavior)
    }

    // ฟังก์ชันพฤติกรรมขณะโจมตี
    protected virtual void AttackBehavior()
    {
        // การโจมตีผู้เล่น
    }

    // ฟังก์ชันพฤติกรรมขณะ Dead
    void DeadBehavior()
    {
        Die();  // เรียกฟังก์ชัน Die
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}