using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
            Instantiate(popUpDamagePrefab,transform.position, Quaternion.identity);
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                healthBar.setHealth(currentHealth);
                // Hasar al�nd���nda "hurt" animasyonunu ba�lat.
                anim.SetTrigger("hurt");
            }
        }
    }


    void Die()
    {
        isDead = true;

        // Karakterin hareketini durdur.
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        // Karakterin �l�m animasyonunu oynat.
        anim.SetBool("dead", true);
        Time.timeScale = 0.35f;

        // Animasyonun uzunlu�unu al.
        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;

        // Karakteri yava��a d���rmek i�in bir coroutine ba�lat.
        StartCoroutine(FallAndStop(animationLength));

        // Karakterin �ld��� konumu sabitle.
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }


    IEnumerator FallAndStop(float duration)
    {
        float elapsedTime = 0;
        float startHeight = transform.position.y;

        while (elapsedTime < duration)
        {
            // Yava��a d��me i�lemi.
            float t = elapsedTime / duration;
            
            

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Animasyon tamamland���nda oyunu durdur.
        Time.timeScale = 0f;
    }
}
