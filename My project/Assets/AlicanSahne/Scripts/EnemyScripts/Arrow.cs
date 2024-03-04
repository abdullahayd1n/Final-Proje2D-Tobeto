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
            
            // Oyuncunun hareket y�n�ne g�re geri itme kuvveti uygula
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            // E�er oyuncunun yatay h�z� pozitif ise (sa�a do�ru hareket ediyorsa), kuvveti sa�a do�ru uygula; aksi takdirde sola do�ru uygula.
            if (playerRigidbody.velocity.x > 0)
            {
                playerRigidbody.AddForce(new Vector2(knockbackForce, 0), ForceMode2D.Impulse);
            }
            else
            {
                playerRigidbody.AddForce(new Vector2(-knockbackForce, 0), ForceMode2D.Impulse);
            }

            Destroy(gameObject); // Ok, oyuncuya �arpt�ktan sonra yok edilir
        }
    }
}
