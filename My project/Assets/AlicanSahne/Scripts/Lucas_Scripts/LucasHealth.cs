using System.Collections;
using TMPro;
using UnityEngine;

public class LucasHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Animator anim;
    public Rigidbody2D rb;
    bool isDead = false;
    public HealthBar healthBar;
    public GameObject popUpDamagePrefab;
    public TMP_Text popUpText;
    public GameObject DeadMenu;
    private bool isPaused = false;

    private Vector3 respawnPosition;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth((int)maxHealth);
    }

    void Update()
    {
        if (isDead && isPaused)
        {
            DeadMenu.SetActive(true);
        }
    }

    public void SetRespawnPoint(Vector3 point)
    {
        respawnPosition = point;
    }

    public void TakeDamage(float damageAmount)
    {
        if (!isDead)
        {
            currentHealth -= damageAmount;
            popUpText.text = damageAmount.ToString();
            Instantiate(popUpDamagePrefab, transform.position, Quaternion.identity);

            if (currentHealth <= 0)
                Die();
            else
            {
                healthBar.setHealth((int)currentHealth);
                anim.SetTrigger("hurt");
            }
        }
    }

    void Die()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        anim.SetBool("dead", true);

        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(FallAndStop(animationLength));
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        Pause();
    }

    public void Pause()
    {
        DeadMenu.SetActive(true);
        Time.timeScale = 0.58f;
        isPaused = true;
    }

    IEnumerator FallAndStop(float duration)
    {
        float elapsedTime = 0f;
        float startHeight = transform.position.y;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 0f;
    }

    public void RespawnAtPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void RespawnAtCheckpoint()
    {
        isDead = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = false;
        anim.SetBool("dead", false);
        transform.position = respawnPosition;
        Time.timeScale = 1f;
        isPaused = false;
        DeadMenu.SetActive(false);
        currentHealth = maxHealth;
        healthBar.setHealth((int)currentHealth);
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        healthBar.setHealth((int)currentHealth);
    }
}
