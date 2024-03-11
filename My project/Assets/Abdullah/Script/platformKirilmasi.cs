using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformKirilmasi : MonoBehaviour
{
    private float fallDealt = 0.1f;
    private float destroyDealy=0.4f;

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
        Destroy(gameObject, destroyDealy);
    }
}
