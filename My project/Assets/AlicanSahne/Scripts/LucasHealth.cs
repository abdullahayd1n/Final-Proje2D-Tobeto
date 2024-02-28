using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucasHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public Animator anim;
    public Rigidbody2D rb;
    bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isDead)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                // Hasar alýndýðýnda "hurt" animasyonunu baþlat.
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

        // Karakterin ölüm animasyonunu oynat.
        anim.SetBool("dead", true);
        Time.timeScale = 0.35f;

        // Animasyonun uzunluðunu al.
        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;

        // Karakteri yavaþça düþürmek için bir coroutine baþlat.
        StartCoroutine(FallAndStop(animationLength));

        // Karakterin öldüðü konumu sabitle.
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }


    IEnumerator FallAndStop(float duration)
    {
        float elapsedTime = 0;
        float startHeight = transform.position.y;

        while (elapsedTime < duration)
        {
            // Yavaþça düþme iþlemi.
            float t = elapsedTime / duration;
            
            

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Animasyon tamamlandýðýnda oyunu durdur.
        Time.timeScale = 0f;
    }
}
