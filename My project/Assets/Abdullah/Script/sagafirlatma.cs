using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sagafirlatma : MonoBehaviour
{
    public float bounceStrength = 0f;
    public float horizontalForce = 0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �arp��ma ger�ekle�ti�inde �arp��ma noktas�na g�re bir vekt�r olu�tur.
        Vector2 collisionPoint = collision.contacts[0].point;

        // Karakterin konumuna g�re bir vekt�r olu�tur ve �arp��ma noktas� ile ��kar.
        Vector2 direction = (Vector2)transform.position - collisionPoint;

        // Sa�a do�ru bir kuvvet eklemek i�in yatay kuvveti de dahil edin.
        Vector2 force = direction.normalized * horizontalForce + Vector2.up * bounceStrength;

        // Rigidbody'ye kuvvet uygula.
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }
}
