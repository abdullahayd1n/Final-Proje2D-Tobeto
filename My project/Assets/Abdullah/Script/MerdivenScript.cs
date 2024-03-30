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
        // Eðer dokunmatik ekran üzerindeki zýplama butonuna basýlýrsa veya basýlý tutulursa
        if (isLadder && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Ýlk dokunuþu al

            // Eðer dokunuþ, zýplama butonu içindeyse
            if (touch.position.x > Screen.width / 2)
            {
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

        // Animasyon kontrolü
        anim.SetBool("isClimbing", isClimbing);
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            // Karakter merdivenlerde yukarý doðru hareket eder
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
        else
        {
            // Karakter normal yerçekimi etkisinde olur
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
            // Merdivenlerden çýkýldýðýnda karakterin merdiven çýkma iþlemi iptal edilir
            isLadder = false;
            isClimbing = false;
        }
    }
}
