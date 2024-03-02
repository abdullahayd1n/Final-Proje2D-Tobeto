using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float timeBtwAttack = 0.5f;
    private float currentTimeBtwAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask WhatIsEnemies;
    public int minDamage = 10;
    public int maxDamage = 50;

    private void Update()
    {
        if (currentTimeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Attack();
            }
        }
        else
        {
            currentTimeBtwAttack -= Time.deltaTime;
        }
    }

    void Attack()
    {
        int damage = Random.Range(minDamage, maxDamage + 1);

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, WhatIsEnemies);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            MeleeEnemy meleeEnemy = enemy.GetComponent<MeleeEnemy>();
            if (meleeEnemy != null)
            {
                meleeEnemy.TakeDamage(damage);
            }
        }

        currentTimeBtwAttack = timeBtwAttack;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPos != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPos.position, attackRange);
        }
    }
}
