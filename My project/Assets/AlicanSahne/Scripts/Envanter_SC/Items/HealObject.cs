using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.gameObject.GetComponent<LucasHealth>().currentHealth < 100)
            {
                collision.gameObject.GetComponent<LucasHealth>().Heal(20);
                Destroy(gameObject);
            }
        }
    }
}
