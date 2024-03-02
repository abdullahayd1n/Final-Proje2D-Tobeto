using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public float attackRange;
    public LayerMask WhatIsEnemies;
    public int minDamage = 10; // Minimum hasar
    public int maxDamage = 50; // Maksimum hasar
    public float knockbackForce = 5f;
    
    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.C))
            {
                // Rastgele hasar deðeri oluþtur
                int damage = Random.Range(minDamage, maxDamage + 1); // +1 ekleyerek maksimum deðeri de dahil ediyoruz

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, WhatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    
                    enemiesToDamage[i].GetComponent<MeleeEnemy>().TakeDamage(damage);
                    enemiesToDamage[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right * knockbackForce, ForceMode2D.Impulse);
                }

            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
