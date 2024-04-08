using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ucmadeneme2 : MonoBehaviour
{
    public float jumpForce = 5f; // Z�plama kuvveti

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �arp��ma yapt���m�z nesnenin etiketini kontrol ediyoruz
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>(); // Oyuncu karakterinin Rigidbody2D bile�enini al�yoruz

            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f); // �nceki dikey h�z� s�f�rl�yoruz
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Yukar� do�ru z�plama kuvveti uyguluyoruz
            }
        }
    }
}
