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

    // Referanslar
    private Animator anim; // Animator bileþeni
    private LucasHealth playerHealth; // Oyuncunun saðlýk bileþeni
    private EnemyPatrol enemyPatrol; // Düþman hareket kontrol bileþeni

 

    private void Awake()
    {
        anim = GetComponent<Animator>(); // Animator bileþenini al
        enemyPatrol = GetComponentInParent<EnemyPatrol>(); // Ebeveyn nesneden düþman hareket kontrol bileþenini al
        
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime; // Saldýrý aralýðý zamanlayýcýsýný güncelle

        // Oyuncu görüþ alanýnda ise saldýr
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0; // Zamanlayýcýyý sýfýrla

                anim.SetTrigger("meleeAttack"); // Melee saldýrý animasyonunu baþlat
                DamagePlayer();

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
}
