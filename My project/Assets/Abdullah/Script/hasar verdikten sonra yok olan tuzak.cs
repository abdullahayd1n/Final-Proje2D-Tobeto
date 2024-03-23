using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hasarverdiktensonrayokolantuzak : MonoBehaviour
{
    [SerializeField] private float damage = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamageAndDestroy(collision);
        if (collision.gameObject.layer == LayerMask.NameToLayer("IsGround"))
        {
            Destroy(gameObject);
        }
    }
    


    private void DamageAndDestroy(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Karaktere hasar verme
            collision.gameObject.GetComponent<LucasHealth>().TakeDamage(damage);

            // Objeyi yok etme
            Destroy(gameObject);
        }
        
    }

}
