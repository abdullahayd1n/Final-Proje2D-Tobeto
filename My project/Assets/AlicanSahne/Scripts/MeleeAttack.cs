using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;//attack pozisyonu
    public float attackRange;
    public LayerMask WhatIsEnemies;//düþman layeri
    public int damage;
    public float knockbackForce = 5f;//ittirme

    private void Update()
    {
       if(timeBtwAttack <= 0)
        {
            if(Input.GetKey(KeyCode.C))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange,WhatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);//damage
                    enemiesToDamage[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right * knockbackForce, ForceMode2D.Impulse);//ittirme knockback
                }

            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

    }

    void OnDrawGizmosSelected()//kýlýcýn ucundaki renk seçeneði
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position,attackRange);
    }

}
