using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    private float timeBtwAttack;
    public float startAttack;
    public Transform meleePos;
    public LayerMask Enemies;
    public float attackRange;
    public int damage;
    
    // Start is called before the first frame update
    void Start()
    {
        timeBtwAttack = startAttack;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack > 0) 
        { 
            timeBtwAttack -= Time.deltaTime; 
        }
        if (timeBtwAttack <= 0 && Input.GetKeyDown(KeyCode.F))
        { 
          Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(meleePos.position, attackRange, Enemies);
                
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyState>().TakeDamage(damage);
            }
            Debug.Log("Attack");
            
            timeBtwAttack = startAttack;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(meleePos.position, attackRange);
    }
}
