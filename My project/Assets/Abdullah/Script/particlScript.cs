using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlScript : MonoBehaviour
{
    [Header("Particles")]
    public GameObject splashParticle; // Particle effect objesi

    // Bu fonksiyon, tetikleyici bir obje suya girildi�inde tetiklenir
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Tetikleyici objenin bir RigidBody2D bile�eni var m� diye kontrol edilir
        Rigidbody2D otherRigidbody = collision.collider.GetComponent<Rigidbody2D>();
        if (otherRigidbody != null)
        {
            // Yak�n temas noktas� bulunur
            Vector2 contactPoint = collision.contacts[0].point;

            // splashParticle objesi instantiate edilir
            Instantiate(splashParticle, contactPoint, Quaternion.identity);
        }
    }

    // Bu fonksiyon, tetikleyici bir obje sudan ��k�ld���nda tetiklenir
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Tetikleyici objenin bir RigidBody2D bile�eni var m� diye kontrol edilir
        Rigidbody2D otherRigidbody = collision.collider.GetComponent<Rigidbody2D>();
        if (otherRigidbody != null)
        {
            // Yak�n temas noktas� bulunur
            Vector2 contactPoint = collision.contacts[0].point;

            // splashParticle objesi instantiate edilir
            Instantiate(splashParticle, contactPoint, Quaternion.identity);
        }
    }

}

