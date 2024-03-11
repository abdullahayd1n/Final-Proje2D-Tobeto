using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sagafirlatma : MonoBehaviour
{
    public float bounceStrength = 0f;
    public float horizontalForce = 0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Çarpýþma gerçekleþtiðinde çarpýþma noktasýna göre bir vektör oluþtur.
        Vector2 collisionPoint = collision.contacts[0].point;

        // Karakterin konumuna göre bir vektör oluþtur ve çarpýþma noktasý ile çýkar.
        Vector2 direction = (Vector2)transform.position - collisionPoint;

        // Saða doðru bir kuvvet eklemek için yatay kuvveti de dahil edin.
        Vector2 force = direction.normalized * horizontalForce + Vector2.up * bounceStrength;

        // Rigidbody'ye kuvvet uygula.
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }
}
