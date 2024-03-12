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
        // Hareket giri�i art�k Update i�inde de�il, Input System ile ba�lant�l� metotlarda kontrol edilecek.
    }

    public void MoveLeft()
    {
        // Sol butonun tetiklenmesi durumunda karakteri sola hareket ettirme.
        Move(-1f);
    }

    public void MoveRight()
    {
        // Sa� butonun tetiklenmesi durumunda karakteri sa�a hareket ettirme.
        Move(1f);
    }

    public void StopMoving()
    {
        // Karakteri durdurma i�levi.
        isMoving = false;
        rb.velocity = new Vector2(0f, rb.velocity.y);
        anim.SetBool("walk", false);
    }

    private void Move(float direction)
    {
        // Karakterin yatay y�nde hareket ettirilmesi.
        float horizontal = direction * speed;
        rb.velocity = new Vector2(horizontal, rb.velocity.y);

        // Y�r�me animasyonunun ba�lat�lmas�.
        if (!isMoving)
        {
            isMoving = true;
            anim.SetBool("walk", true);
        }

        // Karakterin y�z�n�n do�ru y�ne d�nmesi.
        if ((!isFacingRight && direction > 0f) || (isFacingRight && direction < 0f))
        {
            Flip();
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        // Z�plama butonunun tetiklenmesi durumunda z�plama i�levinin �a�r�lmas�.
        if (context.started && IsGrounded())
        {
            Jump();
        }
    }

    public void Jump()
    {
        // Karakterin z�plama i�levi.
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            anim.SetBool("jump", true);
        }
    }

    private bool IsGrounded()
    {
        // Karakterin yerde olup olmad���n� kontrol eden i�lev.
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        // Karakterin y�z�n�n �evrilmesi.
        transform.Rotate(new Vector3(0, 180, 0));
        isFacingRight = !isFacingRight;
    }
}
