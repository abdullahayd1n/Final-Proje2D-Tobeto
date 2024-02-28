using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharControl : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    Animator anim;

    public int combo;
    public bool atacakando;

    private bool canDash = true;
    private bool isDashing = false;

    public float dashSpeed = 10f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 3f;

    [SerializeField] TrailRenderer tr;

    private float horizontal;
    public float speed = 0f;
    private float jumpingPower = 6f;
    private bool isFacingRight = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
        tr = GetComponent<TrailRenderer>();
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }
       
        Combos_();

        UpdateAnimation();
    }

    public void Start_Combo()
    {
        atacakando = false;
        if (combo < 3)
        {
            combo++;
        }
    }

    public void Fnish_ani()
    {
        atacakando = false;
        combo = 0;
    }
    public void Combos_()
    {
        if(Input.GetKeyDown(KeyCode.C)&& !atacakando)
        {
            atacakando = true;
            anim.SetTrigger("" + combo);
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
        transform.Rotate(new Vector3(0,180, 0));
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

    IEnumerator Dash()
    {
        if (canDash)
        {
            canDash = false;
            isDashing = true;
            tr.enabled = true;
            float startTime = Time.time;

            while (Time.time < startTime + dashDuration)
            {
                rb.velocity = new Vector2(horizontal * dashSpeed, rb.velocity.y);
                yield return null;
            }

            isDashing = false;

            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
            tr.enabled = false;
        }
    }



    private void UpdateAnimation()
    {
        anim.SetBool("jump", !IsGrounded());
        anim.SetBool("walk", Mathf.Abs(horizontal) > 0 && IsGrounded()); // Yerden yürüme animasyonunu kontrol et
    }

}
