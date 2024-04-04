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
<<<<<<< Updated upstream
    public GameObject DeadMenu;
    private bool isPaused = false;
=======

>>>>>>> Stashed changes

    private Vector3 respawnPosition;

    void Start()
    {
        currentHealth = maxHealth;
<<<<<<< Updated upstream
        healthBar.SetMaxHealth((int)maxHealth);
    }

    void Update()
    {
        if (isDead && isPaused)
        {
            DeadMenu.SetActive(true);
        }
=======
        healthBar.SetMaxHealth((int)maxHealth); // maxHealth int'ten float'a çevrildi, bu yüzden (int) ile dönüþtürüldü.
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        AudioManager.Instance.PlaySFX("die");
=======
        Time.timeScale = 0.35f;
>>>>>>> Stashed changes
        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(FallAndStopAndOpenMenu(animationLength)); // Coroutine'u güncelledik.
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }


    IEnumerator FallAndStopAndOpenMenu(float duration)
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

        // Menünün açýlmasý
        isPaused = true;
        DeadMenu.SetActive(true);
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
