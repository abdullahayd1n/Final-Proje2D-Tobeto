using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaniyedeBirHasar : MonoBehaviour
{
    [SerializeField]
    private float damagePerSecond = 1; // Saniyedeki hasar miktarý

    private void OnTriggerStay2D(Collider2D other)
    {
        DamagePlayerOverTime(other);
    }

    private void DamagePlayerOverTime(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<LucasHealth>().TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}

