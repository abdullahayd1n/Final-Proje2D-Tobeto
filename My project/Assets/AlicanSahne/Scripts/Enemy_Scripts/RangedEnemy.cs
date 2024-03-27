using BarthaSzabolcs.Tutorial_SpriteFlash;
using System.Collections;
using TMPro;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    public Transform shootPoint; // Okun f�rlat�laca�� nokta
    public GameObject arrowPrefab; // F�rlat�lacak ok prefab'�
    public float arrowSpeed = 10f; // Ok h�z�
    public float arrowLifetime = 2f; // Okun �mr�
    private int damage; // Okun verece�i hasar


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



    Transform target;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;
    private bool playerDetected = false;
    private float detectionPauseTime = 2f;

    public GameObject[] itemDropS;
    private bool hasDroppedItem = false;




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
        cooldownTimer += Time.deltaTime; // Sald�r� aral��� zamanlay�c�s�n� g�ncelle

        // Oyuncu g�r�� alan�nda ise sald�r
        if (PlayerInSight())
        {
            if (!playerDetected)
                StartCoroutine(PlayerDetected());

            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0; // Zamanlay�c�y� s�f�rla
                anim.SetTrigger("attack"); // Melee sald�r� animasyonunu ba�lat

            }
        }
        else
        {
            if (playerDetected)
                StartCoroutine(PlayerNOTDetected());
        }

        // D��man hareket kontrol�n� etkinle�tir veya devre d��� b�rak
        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();

        // D��man� oyuncuya do�ru d�nd�r
        if (target != null)
        {
            transform.localScale = new Vector2(target.position.x > transform.position.x ? 1f : -0.8f, 0.8f);
        }

    }

    private void ShootArrow()
    {
        // Ok prefab'�n� shootPoint'ten f�rlat
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
        AudioManager.Instance.PlaySFX("Arrow");
        // Okun olu�turuldu�u noktadan oyuncuya do�ru hareket etmesini sa�la
        Vector2 direction = (target.position - shootPoint.position).normalized;
        arrow.GetComponent<Rigidbody2D>().velocity = direction * arrowSpeed;

        // Okun olu�turuldu�u noktadan oyuncuya do�ru hareket etmesini sa�lad�ktan sonra bir s�re sonra yok et
        Destroy(arrow, arrowLifetime);

    }

    // Animator i�indeki event olarak at�lacak
    public void AttackAnimationEvent()
    {
        ShootArrow();
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }




    public void TakeDamage(int damage)
    {
        // Prefab� kullanarak nesneyi sahnede olu�turun ve referans� saklay�n
        GameObject bloodEffectInstance = Instantiate(bloodEffect, transform.position, Quaternion.identity);

        // 2 saniye sonra nesneyi silmek i�in coroutine kullan�n
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

        // Nesneyi sahneden kald�r�n
        Destroy(effect);
    }




    private bool isDead = false; // Bayrak, karakterin �ld��� tespit edildi�inde true olacak.

    private void KillEnemy()
    {
        if (enemyHealth <= 0 && !isDead) // Karakter daha �nce �lmediyse ve sa�l�k s�f�r veya daha azsa...
        {
            isDead = true; // Art�k �l� oldu�unu i�aretleriz.
            anim.SetTrigger("die"); // �l�m animasyonunu oynat�r�z.
            Destroy(gameObject, removeEnemy); // Belirli bir s�re sonra karakteri yok ederiz.
            ItemDrop(); // Gerekirse bir ��e d���r�r�z.
            AudioManager.Instance.PlaySFX("Enemy_Die"); // �l�m sesini oynat�r�z.
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

    private void ItemDrop()
    {
        if (!hasDroppedItem) // E�er daha �nce bir ��e d���r�lmediyse devam eder.
        {
            int randomIndex = Random.Range(0, itemDropS.Length); // Rastgele bir index se�er.
            Instantiate(itemDropS[randomIndex], transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            hasDroppedItem = true; // Bayra�� true olarak ayarlar, b�ylece bir sonraki �a�r�da ba�ka bir ��e d���r�lmez.
        }
    }


}