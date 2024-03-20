using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private float damage = 1;


    private void OnCollisionEnter2D(Collision2D other)
    {
        DamagePlayer(other);
    }

    public void DamagePlayer(Collision2D other)
    {
        if(other.transform.tag == "Player")
        {
            other.transform.GetComponent<LucasHealth>().TakeDamage(damage);
        }
    }
}
