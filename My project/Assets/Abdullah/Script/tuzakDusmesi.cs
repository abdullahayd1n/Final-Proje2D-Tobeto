using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tuzakDusmesi : MonoBehaviour
{
    private float fallDelay = 0.001f;
    [SerializeField] private Rigidbody2D rb;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TriggeredFall());
        }

    }

    private IEnumerator TriggeredFall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        
    }

}
