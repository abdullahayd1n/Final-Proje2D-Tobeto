using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMoment : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontal;
    public float speed = 0f;
    private float powerJump = 16f;
    private bool isFacingRight = true;

    // Update is called once per frame
    void Update()
    {
        rb.velocity=new Vector2(horizontal*speed,rb.velocity.y);

        if(!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if(isFacingRight && horizontal < 0f)
        {
            Flip();
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, powerJump);
        }
        if (context.canceled && rb.velocity.y>0f)
        {
            rb.velocity=new Vector2(rb.velocity.x , rb.velocity.y * 0.5f );
        }
    }


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale= localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
}
