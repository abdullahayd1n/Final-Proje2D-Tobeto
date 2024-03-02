using BarthaSzabolcs.Tutorial_SpriteFlash;
using System.Collections;
using TMPro;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    Transform target;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;
    private bool playerDetected = false;
    private float detectionPauseTime = 2f;

    [Header("References")]
    public int health;
    public int enemyHealth;
    public GameObject bloodEffect;
    private Animator anim;
    public GameObject popUpDamagePrefab;
    public TMP_Text popUpText;
    private EnemyHealthBar healthBar;
    private LucasHealth playerHealth;
    private EnemyPatrol enemyPatrol;
    [SerializeField] private SimpleFlash flashEffect;
    [SerializeField] public float removeEnemy = 1.5f;
    public GameObject alert;
    public Rigidbody2D rb;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        health = enemyHealth;
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        KillEnemy();
        cooldownTimer += Time.deltaTime; // Saldýrý aralýðý zamanlayýcýsýný güncelle

        // Oyuncu görüþ alanýnda ise saldýr
        if (PlayerInSight())
        {
            if (!playerDetected)
                StartCoroutine(PlayerDetected());

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
                StartCoroutine(PlayerNOTDetected());
        }

        // Düþman hareket kontrolünü etkinleþtir veya devre dýþý býrak
        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();

        // Düþmaný oyuncuya doðru döndür
        transform.localScale = new Vector2(target.position.x > transform.position.x ? 1f : -1f, 1f);
    }


    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<LucasHealth>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    public void DamagePlayer()
    {
        if (PlayerInSight() && cooldownTimer >= attackCooldown) // Sadece saldýrý cooldown süresi dolmuþken hasar ver
        {
            playerHealth.TakeDamage(damage);
            cooldownTimer = 0; // Saldýrý yapýldýðýnda cooldown süresini sýfýrla
        }
    }


    public void TakeDamage(int damage)
    {
        // Prefabý kullanarak nesneyi sahnede oluþturun ve referansý saklayýn
        GameObject bloodEffectInstance = Instantiate(bloodEffect, transform.position, Quaternion.identity);

        // 2 saniye sonra nesneyi silmek için coroutine kullanýn
        StartCoroutine(DestroyBloodEffect(bloodEffectInstance, 2f));

        enemyHealth -= damage;
        healthBar.UpdateHealthBar(enemyHealth, health);
        popUpText.text = damage.ToString();
        Instantiate(popUpDamagePrefab, transform.position, Quaternion.identity);
        flashEffect.Flash();
    }

    private IEnumerator DestroyBloodEffect(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Nesneyi sahneden kaldýrýn
        Destroy(effect);
    }




    private void KillEnemy()
    {
        if (enemyHealth <= 0)
        {
            anim.SetTrigger("die");
            Destroy(gameObject, removeEnemy);
        }
    }

    private IEnumerator PlayerDetected()
    {
        playerDetected = true;
        rb.velocity = Vector2.zero;
        alert.SetActive(true);
        yield return new WaitForSeconds(detectionPauseTime);
        
    }

    private IEnumerator PlayerNOTDetected()
    {
        yield return new WaitForSeconds(detectionPauseTime);
        playerDetected = false;
        alert.SetActive(false);
    }

    private void playerDamage()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<LucasHealth>().TakeDamage(damage);
    }
}
