using BarthaSzabolcs.Tutorial_SpriteFlash;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PATROLSTATE1 : MonoBehaviour
{
    #region Tanimlar
    [Header("EnemyAttack")]
    public Transform lucasPos;
    public GameObject lucas;
    public float attRange;
    public float disToLucas;

    public Rigidbody2D rb;
    public Transform ledgeDetector;
    public LayerMask groundLayer, obstacleLayer, playerLayer;

    bool isFacingRight = true; // Y�n kontrol� i�in ekledik
    private bool playerDetected;

    public float raycastDistance, obstacleDistance, playerdeDetectDistance;
    public float speed;
    public float detectionPauseTime;

    public GameObject alert;
    //--------------------------------------//
    public int health;
    public int enemyHealth;
    [SerializeField] public float removeEnemy = 1.5f;
    public GameObject bloodEffect;
    private Animator anim;
    public GameObject popUpDamagePrefab;
    public TMP_Text popUpText;
    private EnemyHealthBar healthBar;
    #endregion


    [SerializeField] private SimpleFlash flashEffect;


    void Start()
    {
        health = enemyHealth;
        anim = GetComponent<Animator>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();

        //PlayerAttack
        lucas = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        KillEnemy();

        //PlayerAttack
        lucasPos = lucas.transform;
        disToLucas = Vector2.Distance(transform.position, lucasPos.position);
        if (disToLucas <= attRange)
        {
            if (
                lucasPos.position.x > transform.position.x &&
                transform.localScale.x < 0 ||
                lucasPos.position.x < transform.position.x &&
                transform.localScale.x > 0
                )
            {
                Rotate();
            }
            isFacingRight = false;
            rb.velocity = Vector2.zero;
        }

        CheckForObstate();
        CheckForPlayer();
    }

    void FixedUpdate()
    {
        if (!playerDetected)
        {
            // E�er sa�a do�ru bak�yorsa
            if (isFacingRight)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else // E�er sola do�ru bak�yorsa
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            enemyHealth--;
        }
    }

    public void TakeDamage(int damage)
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        enemyHealth -= damage;
        healthBar.UpdateHealthBar(enemyHealth, health);
        popUpText.text = damage.ToString();
        Instantiate(popUpDamagePrefab, transform.position, Quaternion.identity);
        flashEffect.Flash();
    }

    void KillEnemy()
    {
        if (enemyHealth <= 0)
        {
            anim.SetTrigger("dead");
            Destroy(gameObject, removeEnemy);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)//lucas�n can� gidiyor.
    {
        if (collision.gameObject.CompareTag("Player")) // "CompareTag" metodu kullan�larak etiket kar��la�t�rmas� yap�l�yor
        {
            LucasHealth healthComponent = collision.gameObject.GetComponent<LucasHealth>();
            if (healthComponent != null)
            {

                healthComponent.TakeDamage(10); // Oyuncuya 10 hasar veriliyor
            }
        }
    }
    #region KontrolKodlari
    void CheckForObstate()
    {
        RaycastHit2D hit = Physics2D.Raycast(ledgeDetector.position, Vector2.down, raycastDistance, groundLayer);
        RaycastHit2D hitobstacle = Physics2D.Raycast(ledgeDetector.position, Vector2.right, obstacleDistance, obstacleLayer);

        if (hit.collider == null || hitobstacle.collider != null)
        {
            Rotate();
        }
    }


    void CheckForPlayer()
    {
        RaycastHit2D hitPlayer = Physics2D.Raycast(ledgeDetector.position, isFacingRight ? Vector2.right : Vector2.left, playerdeDetectDistance, playerLayer);

        if (hitPlayer.collider != null)
            StartCoroutine(PlayerDetected());
        else if (playerDetected)
            StartCoroutine(PlayerNOTDetected());

    }

    IEnumerator PlayerDetected()
    {
        playerDetected = true;
        rb.velocity = Vector2.zero;
        alert.SetActive(true);
        yield return new WaitForSeconds(detectionPauseTime);
        Debug.Log("�ARJ!");
    }

    IEnumerator PlayerNOTDetected()
    {
        yield return new WaitForSeconds(detectionPauseTime);
        playerDetected = false;
        alert.SetActive(false);
    }

    void Rotate()
    {
        // Y�n� tersine �evir
        isFacingRight = !isFacingRight;
        // Nesneyi d�nd�r
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(ledgeDetector.position, (isFacingRight ? Vector2.right : Vector2.left) * playerdeDetectDistance);
    }
    #endregion
}
