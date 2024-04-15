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
        // Eğer dokunmatik ekran üzerindeki zıplama butonuna basılırsa veya basılı tutulursa
        if (isLadder && (Input.touchCount > 0 || Input.GetKey(KeyCode.Space)))
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0); // İlk dokunuşu al

                // Eğer dokunuş, zıplama butonu içindeyse
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
            else if (Input.GetKey(KeyCode.Space)) // Uzay tuşu basılmışsa
            {
                AudioManager.Instance.PlaySFX("climbing");
                isClimbing = true;
            }
        }
        else
        {
            isClimbing = false;
        }

        // Animasyon kontrolü
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
