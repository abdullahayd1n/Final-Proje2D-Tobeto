using BarthaSzabolcs.Tutorial_SpriteFlash;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public float speed;
    Rigidbody rb;

    [SerializeField] private SimpleFlash flashEffect;
    
   
  public GameObject bloodEffect;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        if (health < 0)
        {
            Destroy(gameObject);
        }

        {
            
        }
        transform.Translate(Vector2.right*speed*Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
        flashEffect.Flash();
    }

    private void OnCollisionEnter2D(Collision2D collision)//lucasýn caný gidiyor.
    {
        if (collision.gameObject.CompareTag("Player")) // "CompareTag" metodu kullanýlarak etiket karþýlaþtýrmasý yapýlýyor
        {
            LucasHealth healthComponent = collision.gameObject.GetComponent<LucasHealth>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(10); // Oyuncuya 10 hasar veriliyor
            }
        }
    }


}
