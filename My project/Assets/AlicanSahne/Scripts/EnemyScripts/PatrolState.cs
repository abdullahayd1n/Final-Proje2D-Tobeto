using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform ledgeDetector;
    public LayerMask groundLayer, obstacleLayer;

    public float raycastDistance, obstacleDistance;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D bileþenini al
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(ledgeDetector.position, Vector2.down, raycastDistance, groundLayer);
        RaycastHit2D hitObstacle = Physics2D.Raycast(ledgeDetector.position, Vector2.left, obstacleDistance, obstacleLayer);

        if (hit.collider == null || hitObstacle.collider != null)
        {
            rotate();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void rotate()
    {
        transform.Rotate(0, 180, 0);
        speed *= -1; // Yönü tersine çevir
    }
}
