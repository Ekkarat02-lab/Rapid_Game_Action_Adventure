using UnityEngine;

public class ZoneEnemy : MonoBehaviour
{
    public int bonusMaxHP = 20;
    public int bonusDamage = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyState enemy = other.GetComponent<EnemyState>();
            if (enemy != null)
            {
                enemy.maxHP += bonusMaxHP;

                enemy.CurrentHealth = enemy.maxHP;

                enemy.Damage += bonusDamage;

                Debug.Log("Enemy maxHP increased by: " + bonusMaxHP);
                Debug.Log("Enemy CurrentHealth set to new maxHP: " + enemy.maxHP);
                Debug.Log("Enemy Damage increased by: " + bonusDamage);
            }
        }
    }
}