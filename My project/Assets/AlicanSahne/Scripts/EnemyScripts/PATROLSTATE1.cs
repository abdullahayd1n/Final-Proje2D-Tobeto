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

    bool isFacingRight = true; // Yön kontrolü için ekledik

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
        // Eðer saða doðru bakýyorsa
        if (isFacingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else // Eðer sola doðru bakýyorsa
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    void Rotate()
    {
        // Yönü tersine çevir
        isFacingRight = !isFacingRight;
        // Nesneyi döndür
        transform.Rotate(0, 180, 0);
    }
}
