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
    // Puan deðeri
    private int scoreValue = 0;

    // Yadigar puan metni
    public Text yadigarScore;
    // Yadigar puan deðeri
    private int yadigarValue = 0;

    // Oyun baþlangýcýnda çalýþacak metod
    private void Awake()
    {
        // Yadigar puaný kaydedilmiþse yükle ve ekrana yaz
        if (PlayerPrefs.HasKey("ButonLucas_Yadigar"))
        {
            yadigarValue = PlayerPrefs.GetInt("ButonLucas_Yadigar");
            yadigarScore.text = " " + yadigarValue;
        }
        // Normal puan kaydedilmiþse yükle ve ekrana yaz
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

        // Karakterin yönünü çevir
        if (!isFacingRight && direction > 0f)
        {
            Flip();
        }
        else if (isFacingRight && direction < 0f)
        {
            Flip();
        }

        // Hareket ediyorsa ve daha önce hareket etmiyorsa animasyonu baþlat
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

    // Tetikleyici ile etkileþime girildiðinde çalýþacak metod
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Yadigar objesi ile etkileþime girildiyse
        if (collision.gameObject.tag == "Yadigar")
        {
            // Yadigar objesini devre dýþý býrak
            collision.gameObject.SetActive(false);
            // Yadigar puanýný artýr
            yadigarValue += 1;
            // Yadigar puanýný ekrana yaz
            yadigarScore.text = " " + yadigarValue;
            // Yadigar puanýný kaydet
            PlayerPrefs.SetInt("ButonLucas_Yadigar", yadigarValue);
        }
        // Normal para objesi ile etkileþime girildiyse
        else if (collision.gameObject.tag == "Coins")
        {
            // Para objesini devre dýþý býrak
            collision.gameObject.SetActive(false);
            // Puaný artýr
            scoreValue += 1;
            // Puaný ekrana yaz
            score.text = " " + scoreValue;
            // Puaný kaydet
            PlayerPrefs.SetInt("ButonLucas_Coins", scoreValue);
        }
    }

    // Puanlarý güncelleyen metod
    void SetScore()
    {
        // Yadigar puanýný yadigarScore metin alanýnda göstermek için deðeri ayarla
        yadigarScore.text = " " + yadigarValue;
        // Puaný score metin alanýnda göstermek için deðeri ayarla
        score.text = " " + scoreValue;
    }

}
