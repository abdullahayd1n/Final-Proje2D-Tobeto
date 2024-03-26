using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaraktmaZeminDestroy : MonoBehaviour
{
    public float fallDealt = 0.1f;
    [SerializeField] private float destroyDealy = 0f;
    [SerializeField] private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
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
