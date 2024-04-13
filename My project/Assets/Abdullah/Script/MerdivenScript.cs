using UnityEngine;

public class MerdivenScript : MonoBehaviour
{
    private float speed = 8f;
    private bool isLadder;
    private bool isClimbing;
    private Animator anim;

    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        tirmanma();
    }

    public void tirmanma()
    {
        // E�er dokunmatik ekran �zerindeki z�plama butonuna bas�l�rsa veya bas�l� tutulursa
        if (isLadder && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // �lk dokunu�u al

            // E�er dokunu�, z�plama butonu i�indeyse
            if (touch.position.x > Screen.width / 2)
            {
                AudioManager.Instance.PlaySFX("climbing");
                isClimbing = true;
            }
            else
            {
                isClimbing = false;
            }
        }
        else
        {
            isClimbing = false;
        }

        // Animasyon kontrol�
        anim.SetBool("isClimbing", isClimbing);
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            // Karakter merdivenlerde yukar� do�ru hareket eder
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
        else
        {
            // Karakter normal yer�ekimi etkisinde olur
            rb.gravityScale = 1.8f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            // Merdivenlerden ��k�ld���nda karakterin merdiven ��kma i�lemi iptal edilir
            isLadder = false;
            isClimbing = false;
        }
    }
}
