using BarthaSzabolcs.Tutorial_SpriteFlash;
using System.Collections;
using TMPro;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown; // Saldýrý aralýðý
    [SerializeField] private float range; // Saldýrý menzili
    [SerializeField] private int damage; // Hasar miktarý

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance; // Kutu collider mesafesi
    [SerializeField] private BoxCollider2D boxCollider; // Kutu collider bileþeni

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer; // Oyuncu katmaný
    private float cooldownTimer = Mathf.Infinity; // Saldýrý aralýðý zamanlayýcý

    [Header("Referanslar")]
    public int health;
    public int enemyHealth;
    public GameObject bloodEffect;
    private Animator anim;
    public GameObject popUpDamagePrefab;
    public TMP_Text popUpText;
    private EnemyHealthBar healthBar;
    private LucasHealth playerHealth; // Oyuncunun saðlýk bileþeni
    private EnemyPatrol enemyPatrol; // Düþman hareket kontrol bileþeni
    [SerializeField] private SimpleFlash flashEffect;
    [SerializeField] public float removeEnemy = 1.5f;

    private bool playerDetected = false;
    public GameObject alert;
    public float detectionPauseTime = 2f;
    public Rigidbody2D rb;

    void Start()
    {
        health = enemyHealth;
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        anim = GetComponent<Animator>(); // Animator bileþenini al
        enemyPatrol = GetComponentInParent<EnemyPatrol>(); // Ebeveyn nesneden düþman hareket kontrol bileþenini al
    }

    private void Update()
    {
        KillEnemy();
        cooldownTimer += Time.deltaTime; // Saldýrý aralýðý zamanlayýcýsýný güncelle

        // Oyuncu görüþ alanýnda ise saldýr
        if (PlayerInSight())
        {
            if (!playerDetected)
            {
                StartCoroutine(PlayerDetected());
            }

            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0; // Zamanlayýcýyý sýfýrla
                anim.SetTrigger("meleeAttack"); // Melee saldýrý animasyonunu baþlat
                DamagePlayer();
            }
        }
        else
        {
            if (playerDetected)
            {
                StartCoroutine(PlayerNOTDetected());
            }
        }

        // Düþman hareket kontrolünü etkinleþtir veya devre dýþý býrak
        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    // Oyuncu görüþ alanýnda mý kontrol et
    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                0, Vector2.left, 0, playerLayer);

        // Oyuncu bulunduysa, onun saðlýk bileþenini al
        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<LucasHealth>();

        return hit.collider != null; // Oyuncu bulundu mu?
    }

    // Gizmo çiz
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    // Oyuncuya hasar ver
    public void DamagePlayer()
    {
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        enemyHealth -= damage;
        anim.SetTrigger("Hurt");
        healthBar.UpdateHealthBar(enemyHealth, health);
        popUpText.text = damage.ToString();
        Instantiate(popUpDamagePrefab, transform.position, Quaternion.identity);
        flashEffect.Flash();
    }

    void KillEnemy()
    {
        if (enemyHealth <= 0)
        {
            anim.SetTrigger("die");
            Destroy(gameObject, removeEnemy);
        }
    }

    IEnumerator PlayerDetected()
    {
        playerDetected = true;
        rb.velocity = Vector2.zero;
        alert.SetActive(true);
        yield return new WaitForSeconds(detectionPauseTime);
        Debug.Log("ÞARJ!");
    }

    IEnumerator PlayerNOTDetected()
    {
        yield return new WaitForSeconds(detectionPauseTime);
        playerDetected = false;
        alert.SetActive(false);
    }

    void playerDamage()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<LucasHealth>().TakeDamage(damage);
    }
}
