using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum State { Idle, Move, Chase, Attack, Dead }  // เพิ่ม Move
    public State currentState = State.Idle;            // สถานะเริ่มต้นเป็น Idle

    public int maxHP = 100;
    public int currentHP;
    public float detectionRange = 5f;  // ระยะที่ศัตรูตรวจจับผู้เล่น

    public void Start()
    {
        currentHP = maxHP;
        currentState = State.Move; // เริ่มต้นด้วยการเดิน
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

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log("Enemy HP: " + currentHP);

        if (currentHP <= 0)
        {
            currentState = State.Dead;  // State Dead
        }
    }

    public void IncreaseHP(int amount)
    {
        currentHP += amount;
        Debug.Log("Enemy HP เพิ่มขึ้น: " + currentHP);
    }

    public void Die()
    {
        Destroy(gameObject);
        Debug.Log("Enemy ตายแล้ว");
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