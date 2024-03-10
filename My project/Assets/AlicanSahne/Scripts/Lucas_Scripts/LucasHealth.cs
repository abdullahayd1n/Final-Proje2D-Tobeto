using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class LucasHealth : MonoBehaviour
{
    public float maxHealth = 100f; // float olarak de�i�tirildi
    public float currentHealth; // float olarak de�i�tirildi
    public Animator anim;
    public Rigidbody2D rb;
    bool isDead = false;
    public HealthBar healthBar;
    public GameObject popUpDamagePrefab;
    public TMP_Text popUpText;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth((int)maxHealth); // maxHealth int'ten float'a �evrildi, bu y�zden (int) ile d�n��t�r�ld�.
    }

    public void TakeDamage(float damageAmount) // int yerine float olarak de�i�tirildi
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
                healthBar.setHealth((int)currentHealth); // currentHealth int'ten float'a �evrildi, bu y�zden (int) ile d�n��t�r�ld�.
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
        float elapsedTime = 0f; // float olarak de�i�tirildi
        float startHeight = transform.position.y;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            elapsedTime += Time.unscaledDeltaTime; // Zaman�n �l�eksiz olarak ge�mesini sa�la
            yield return null;
        }

        Time.timeScale = 0f;
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount; // Can miktar�n� artt�r
        currentHealth = Mathf.Min(currentHealth, maxHealth); // Sa�l�k miktar�n� maksimum sa�l�kla s�n�rla
        healthBar.setHealth((int)currentHealth); // Sa�l�k �ubu�unu g�ncelle
    }


}