using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    // Puan metni
    public Text score;
    // Puan de�eri
    private int scoreValue = 0;

    // Yadigar puan metni
    public Text yadigarScore;
    // Yadigar puan de�eri
    private int yadigarValue = 0;

    // Oyun ba�lang�c�nda �al��acak metod
    private void Awake()
    {
        // Yadigar puan� kaydedilmi�se y�kle ve ekrana yaz
        if (PlayerPrefs.HasKey("ButonLucas_Yadigar"))
        {
            yadigarValue = PlayerPrefs.GetInt("ButonLucas_Yadigar");
            yadigarScore.text = " " + yadigarValue;
        }
        // Normal puan kaydedilmi�se y�kle ve ekrana yaz
        if (PlayerPrefs.HasKey("ButonLucas_Coins"))
        {
            scoreValue = PlayerPrefs.GetInt("ButonLucas_Coins");
            score.text = " " + scoreValue;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        UpdateAnimation();
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

        // Karakterin y�n�n� �evir
        if (!isFacingRight && direction > 0f)
        {
            Flip();
        }
        else if (isFacingRight && direction < 0f)
        {
            Flip();
        }

        // Hareket ediyorsa ve daha �nce hareket etmiyorsa animasyonu ba�lat
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

    // Tetikleyici ile etkile�ime girildi�inde �al��acak metod
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Yadigar objesi ile etkile�ime girildiyse
        if (collision.gameObject.tag == "Yadigar")
        {
            // Yadigar objesini devre d��� b�rak
            collision.gameObject.SetActive(false);
            // Yadigar puan�n� art�r
            yadigarValue += 1;
            // Yadigar puan�n� ekrana yaz
            yadigarScore.text = " " + yadigarValue;
            // Yadigar puan�n� kaydet
            PlayerPrefs.SetInt("ButonLucas_Yadigar", yadigarValue);
        }
        // Normal para objesi ile etkile�ime girildiyse
        else if (collision.gameObject.tag == "Coins")
        {
            // Para objesini devre d��� b�rak
            collision.gameObject.SetActive(false);
            // Puan� art�r
            scoreValue += 1;
            // Puan� ekrana yaz
            score.text = " " + scoreValue;
            // Puan� kaydet
            PlayerPrefs.SetInt("ButonLucas_Coins", scoreValue);
        }
    }

    // Puanlar� g�ncelleyen metod
    void SetScore()
    {
        // Yadigar puan�n� yadigarScore metin alan�nda g�stermek i�in de�eri ayarla
        yadigarScore.text = " " + yadigarValue;
        // Puan� score metin alan�nda g�stermek i�in de�eri ayarla
        score.text = " " + scoreValue;
    }

}
