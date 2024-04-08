using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ucmadeneme2 : MonoBehaviour
{
    public float jumpForce = 5f; // Zýplama kuvveti

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Çarpýþma yaptýðýmýz nesnenin etiketini kontrol ediyoruz
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>(); // Oyuncu karakterinin Rigidbody2D bileþenini alýyoruz

            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f); // Önceki dikey hýzý sýfýrlýyoruz
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Yukarý doðru zýplama kuvveti uyguluyoruz
            }
        }
    }
}
