using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int attackDamage = 20;
    public int enragedAttackDamage = 40;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D[] hits = Physics2D.OverlapCircleAll(pos, attackRange, attackMask);
        foreach (Collider2D hit in hits)
        {
            if (hit != null)
            {
                LucasHealth healthComponent = hit.GetComponent<LucasHealth>();
                if (healthComponent != null)
                {
                    healthComponent.TakeDamage(attackDamage);
                }
            }
        }
    }

    public void EnragedAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D[] hits = Physics2D.OverlapCircleAll(pos, attackRange, attackMask);
        foreach (Collider2D hit in hits)
        {
            if (hit != null)
            {
                LucasHealth healthComponent = hit.GetComponent<LucasHealth>();
                if (healthComponent != null)
                {
                    healthComponent.TakeDamage(enragedAttackDamage);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
