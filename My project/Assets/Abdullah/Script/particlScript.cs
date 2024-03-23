using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlScript : MonoBehaviour
{
    [Header("Particles")]
    public GameObject splashParticle; // Particle effect objesi

    // Bu fonksiyon, tetikleyici bir obje suya girildiðinde tetiklenir
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Tetikleyici objenin bir RigidBody2D bileþeni var mý diye kontrol edilir
        Rigidbody2D otherRigidbody = collision.collider.GetComponent<Rigidbody2D>();
        if (otherRigidbody != null)
        {
            // Yakýn temas noktasý bulunur
            Vector2 contactPoint = collision.contacts[0].point;

            // splashParticle objesi instantiate edilir
            Instantiate(splashParticle, contactPoint, Quaternion.identity);
        }
    }

    // Bu fonksiyon, tetikleyici bir obje sudan çýkýldýðýnda tetiklenir
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Tetikleyici objenin bir RigidBody2D bileþeni var mý diye kontrol edilir
        Rigidbody2D otherRigidbody = collision.collider.GetComponent<Rigidbody2D>();
        if (otherRigidbody != null)
        {
            // Yakýn temas noktasý bulunur
            Vector2 contactPoint = collision.contacts[0].point;

            // splashParticle objesi instantiate edilir
            Instantiate(splashParticle, contactPoint, Quaternion.identity);
        }
    }

}

