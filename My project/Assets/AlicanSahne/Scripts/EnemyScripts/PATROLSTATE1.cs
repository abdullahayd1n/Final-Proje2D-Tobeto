using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PATROLSTATE1 : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform ledgeDetector;
    public LayerMask groundLayer;

    public float raycastDistance;
    public float speed;

    bool isFacingRight = true; // Y�n kontrol� i�in ekledik

    void Start()
    {

    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(ledgeDetector.position, Vector2.down, raycastDistance, groundLayer);

        if (hit.collider == null)
        {
            Rotate();
        }
    }

    void FixedUpdate()
    {
        // E�er sa�a do�ru bak�yorsa
        if (isFacingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else // E�er sola do�ru bak�yorsa
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    void Rotate()
    {
        // Y�n� tersine �evir
        isFacingRight = !isFacingRight;
        // Nesneyi d�nd�r
        transform.Rotate(0, 180, 0);
    }
}
