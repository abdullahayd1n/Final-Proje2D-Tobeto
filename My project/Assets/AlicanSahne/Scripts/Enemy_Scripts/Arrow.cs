using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int minDamage = 25; // Minimum Hasar
    public int maxDamage = 50; // Maksimum Hasar
    public float knockbackForce = 10f; // Geri itme kuvveti

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int damage = Random.Range(minDamage, maxDamage + 1);
            collision.gameObject.GetComponent<LucasHealth>().TakeDamage(damage);
            
            // Oyuncunun hareket yönüne göre geri itme kuvveti uygula
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            // Eðer oyuncunun yatay hýzý pozitif ise (saða doðru hareket ediyorsa), kuvveti saða doðru uygula; aksi takdirde sola doðru uygula.
            if (playerRigidbody.velocity.x > 0)
            {
                playerRigidbody.AddForce(new Vector2(knockbackForce, 0), ForceMode2D.Impulse);
            }
            else
            {
                playerRigidbody.AddForce(new Vector2(-knockbackForce, 0), ForceMode2D.Impulse);
            }

            Destroy(gameObject); // Ok, oyuncuya çarptýktan sonra yok edilir
        }
    }
}
