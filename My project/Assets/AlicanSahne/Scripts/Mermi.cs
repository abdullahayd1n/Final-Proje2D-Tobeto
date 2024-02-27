using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public float endTime;

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
            collision.GetComponent<EnemyHealth>().TakeDamage(50);
            Destroy(gameObject);
        }
    }
}
