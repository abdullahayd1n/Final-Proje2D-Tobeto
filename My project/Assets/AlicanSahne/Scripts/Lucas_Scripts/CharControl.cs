using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharControl : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    

    Animator anim;

   

    private float horizontal;
    public float speed = 0f;
    public float jumpingPower = 6f;
    private bool isFacingRight = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
            
        UpdateAnimation();


    }

    

    public void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            anim.SetBool("jump", true);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        transform.Rotate(new Vector3(0, 180, 0));
        isFacingRight = !isFacingRight;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!IsGrounded())
        {
            horizontal = context.ReadValue<Vector2>().x;
            anim.SetBool("walk", false);
        }
        else
        {
            if (context.canceled)
            {
                anim.SetBool("walk", false);
                horizontal = 0f;
                return;
            }

            anim.SetBool("walk", true);
            horizontal = context.ReadValue<Vector2>().x;
        }
    }

    

    private void UpdateAnimation()
    {
        anim.SetBool("jump", !IsGrounded());
        anim.SetBool("walk", Mathf.Abs(horizontal) > 0 && IsGrounded()); // Yerden yürüme animasyonunu kontrol et
    }

    
}
