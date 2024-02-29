using UnityEngine;
using TMPro;
using System.Collections;

public class LucasHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator anim;
    public Rigidbody2D rb;
    bool isDead = false;
    public HealthBar healthBar;
    public GameObject popUpDamagePrefab;
    public TMP_Text popUpText;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damageAmount)
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
                healthBar.setHealth(currentHealth);
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
        Time.timeScale = 0.35f;
        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(FallAndStop(animationLength));
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    IEnumerator FallAndStop(float duration)
    {
        float elapsedTime = 0;
        float startHeight = transform.position.y;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Time.timeScale = 0f;
    }
}
