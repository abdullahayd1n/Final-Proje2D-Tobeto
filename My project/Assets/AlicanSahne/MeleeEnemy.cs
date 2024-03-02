using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown; // Sald�r� aral���
    [SerializeField] private float range; // Sald�r� menzili
    [SerializeField] private int damage; // Hasar miktar�

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance; // Kutu collider mesafesi
    [SerializeField] private BoxCollider2D boxCollider; // Kutu collider bile�eni

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer; // Oyuncu katman�
    private float cooldownTimer = Mathf.Infinity; // Sald�r� aral��� zamanlay�c�

    // Referanslar
    private Animator anim; // Animator bile�eni
    private LucasHealth playerHealth; // Oyuncunun sa�l�k bile�eni
    private EnemyPatrol enemyPatrol; // D��man hareket kontrol bile�eni

 

    private void Awake()
    {
        anim = GetComponent<Animator>(); // Animator bile�enini al
        enemyPatrol = GetComponentInParent<EnemyPatrol>(); // Ebeveyn nesneden d��man hareket kontrol bile�enini al
        
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime; // Sald�r� aral��� zamanlay�c�s�n� g�ncelle

        // Oyuncu g�r�� alan�nda ise sald�r
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0; // Zamanlay�c�y� s�f�rla

                anim.SetTrigger("meleeAttack"); // Melee sald�r� animasyonunu ba�lat
                DamagePlayer();

            }
            
        }

        // D��man hareket kontrol�n� etkinle�tir veya devre d��� b�rak
        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    // Oyuncu g�r�� alan�nda m� kontrol et
    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        // Oyuncu bulunduysa, onun sa�l�k bile�enini al
        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<LucasHealth>();

        return hit.collider != null; // Oyuncu bulundu mu?
    }

    // Gizmo �iz
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
