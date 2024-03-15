using UnityEngine;
using UnityEngine.InputSystem;

public class ButonLucas : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator anim;
    public float speed = 0f;
    public float jumpingPower = 6f;
    private bool isFacingRight = true;
    private bool isMoving = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        UpdateAnimation(); // UpdateAnimation y�ntemini �a��r
    }

    public void MoveLeft()
    {
        Move(-1f);
    }

    public void MoveRight()
    {
        Move(1f);
    }

    public void StopMoving()
    {
        isMoving = false;
        rb.velocity = new Vector2(0f, rb.velocity.y);
        anim.SetBool("walk", false);
    }

    private void Move(float direction)
    {
        float horizontal = direction * speed;
        rb.velocity = new Vector2(horizontal, rb.velocity.y);

        if (!isFacingRight && direction > 0f)
        {
            Flip();
        }
        else if (isFacingRight && direction < 0f)
        {
            Flip();
        }

        if (!isMoving)
        {
            isMoving = true;
            anim.SetBool("walk", true);
        }
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

    private void UpdateAnimation()
    {
        anim.SetBool("jump", !IsGrounded());
        anim.SetBool("walk", isMoving && IsGrounded());
    }
}