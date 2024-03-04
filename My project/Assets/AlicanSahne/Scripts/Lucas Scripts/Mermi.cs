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
            int damage = Random.Range(minDamage, maxDamage + 1); // Rastgele hasar miktarýný belirle
            collision.GetComponent<MeleeEnemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
