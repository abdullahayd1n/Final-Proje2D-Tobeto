using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public Transform firstPos, secondPos;
    public float speed;
    public float waitTime = 3f; // Bekleme süresi

    private Vector3 nextPos;
    private bool isMoving = true; // Asansörün hareket halinde olup olmadýðýný kontrol etmek için

    private void Start()
    {
        nextPos = firstPos.position;
    }

    private void Update()
    {
        if (isMoving)
        {
            if (transform.position == firstPos.position)
                StartCoroutine(MoveToPosition(secondPos.position)); // Ýkinci konuma doðru hareket et

            if (transform.position == secondPos.position)
                StartCoroutine(MoveToPosition(firstPos.position)); // Ýlk konuma doðru hareket et
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        isMoving = false; // Hareket baþladý

        // Hedef konuma doðru hareket et
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        // Belirtilen süre kadar beklet
        yield return new WaitForSeconds(waitTime);

        isMoving = true; // Hareket tamamlandý, tekrar harekete geçilebilir
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(firstPos.position, secondPos.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            collision.collider.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
