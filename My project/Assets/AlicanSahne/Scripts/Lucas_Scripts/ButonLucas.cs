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
        // Hareket giriþi artýk Update içinde deðil, Input System ile baðlantýlý metotlarda kontrol edilecek.
    }

    public void MoveLeft()
    {
        // Sol butonun tetiklenmesi durumunda karakteri sola hareket ettirme.
        Move(-1f);
    }

    public void MoveRight()
    {
        // Sað butonun tetiklenmesi durumunda karakteri saða hareket ettirme.
        Move(1f);
    }

    public void StopMoving()
    {
        // Karakteri durdurma iþlevi.
        isMoving = false;
        rb.velocity = new Vector2(0f, rb.velocity.y);
        anim.SetBool("walk", false);
    }

    private void Move(float direction)
    {
        // Karakterin yatay yönde hareket ettirilmesi.
        float horizontal = direction * speed;
        rb.velocity = new Vector2(horizontal, rb.velocity.y);

        // Yürüme animasyonunun baþlatýlmasý.
        if (!isMoving)
        {
            isMoving = true;
            anim.SetBool("walk", true);
        }

        // Karakterin yüzünün doðru yöne dönmesi.
        if ((!isFacingRight && direction > 0f) || (isFacingRight && direction < 0f))
        {
            Flip();
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        // Zýplama butonunun tetiklenmesi durumunda zýplama iþlevinin çaðrýlmasý.
        if (context.started && IsGrounded())
        {
            Jump();
        }
    }

    public void Jump()
    {
        // Karakterin zýplama iþlevi.
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            anim.SetBool("jump", true);
        }
    }

    private bool IsGrounded()
    {
        // Karakterin yerde olup olmadýðýný kontrol eden iþlev.
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        // Karakterin yüzünün çevrilmesi.
        transform.Rotate(new Vector3(0, 180, 0));
        isFacingRight = !isFacingRight;
    }
}
