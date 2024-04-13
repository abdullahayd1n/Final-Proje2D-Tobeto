using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public float endTime;
    public int minDamage = 25; // Minimum Hasar
    public int maxDamage = 50; // Maksimum Hasar

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, endTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("WhatIsEnemies"))
        {
            RangedEnemy rangedEnemy = collision.GetComponent<RangedEnemy>();
            if (rangedEnemy != null)
            {
                int damage = Random.Range(minDamage, maxDamage + 1); // Randomize damage amount
                rangedEnemy.TakeDamage(damage);
                Destroy(gameObject);
            }

            MeleeEnemy meleeEnemy = collision.GetComponent<MeleeEnemy>();
            if (meleeEnemy != null)
            {
                int damage = Random.Range(minDamage, maxDamage + 1); // Randomize damage amount
                meleeEnemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("Boss")) // Check if collided with Boss
        {
            BossHealth boss = collision.GetComponent<BossHealth>();
            if (boss != null)
            {
                int damage = Random.Range(minDamage, maxDamage + 1); // Randomize damage amount
                boss.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
