using UnityEngine;

public class ObjeHasar : MonoBehaviour
{
    [SerializeField]
    private float damage = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        DamageLucas(other);
    }

    private void DamageLucas(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            LucasHealth lucasHealth = other.transform.GetComponent<LucasHealth>();
            if (lucasHealth != null)
            {
                lucasHealth.TakeDamage(damage);
            }
        }
    }
}
