using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public Transform firstPos, secondPos;
    public float speed;
    public float waitTime = 3f; // Bekleme s�resi

    private Vector3 nextPos;
    private bool isMoving = true; // Asans�r�n hareket halinde olup olmad���n� kontrol etmek i�in

    private void Start()
    {
        nextPos = firstPos.position;
    }

    private void Update()
    {
        if (isMoving)
        {
            if (transform.position == firstPos.position)
                StartCoroutine(MoveToPosition(secondPos.position)); // �kinci konuma do�ru hareket et

            if (transform.position == secondPos.position)
                StartCoroutine(MoveToPosition(firstPos.position)); // �lk konuma do�ru hareket et
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        isMoving = false; // Hareket ba�lad�

        // Hedef konuma do�ru hareket et
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        // Belirtilen s�re kadar beklet
        yield return new WaitForSeconds(waitTime);

        isMoving = true; // Hareket tamamland�, tekrar harekete ge�ilebilir
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
