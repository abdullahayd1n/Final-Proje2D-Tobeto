using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformdusmesi : MonoBehaviour
{
    private float fallDealt = 0.01f;
   

    [SerializeField] private Rigidbody2D rb;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDealt);
        rb.bodyType = RigidbodyType2D.Dynamic;
       
    }
}