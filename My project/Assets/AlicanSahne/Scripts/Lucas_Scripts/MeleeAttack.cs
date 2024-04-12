using UnityEngine;
using UnityEngine.UI;

public class MeleeAttack : MonoBehaviour
{
    public float timeBtwAttack = 0.5f;
    private float currentTimeBtwAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask WhatIsEnemies;
    public int minDamage = 10;
    public int maxDamage = 50;

    public int combo;
    public bool atacakando;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

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

        Combos_();
    }


    public void Start_Combo()
    {
        atacakando = false;
        if (combo < 3)
        {
            combo++;
        }
    }

    public void Fnish_ani()
    {
        atacakando = false;
        combo = 0;
    }

    public void Combos_()
    {
        // Eðer C tuþuna basýldýysa ve saldýrý yapýlabilir durumdaysa
        if (Input.GetKeyDown(KeyCode.C) && !atacakando)
        {
            atacakando = true;
            anim.SetTrigger("" + combo);
        }
    }

    public void Attack()
    {
        int damage = Random.Range(minDamage, maxDamage + 1);

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, WhatIsEnemies);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            BossHealth boss = enemy.GetComponent<BossHealth>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
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


    public void ButtonCombo()
    {
        if (!atacakando)
        {
            Attack();
            atacakando = true;
            anim.SetTrigger("" + combo);
            //AudioManager.Instance.PlaySFX("Attack");
        }
    }
}
