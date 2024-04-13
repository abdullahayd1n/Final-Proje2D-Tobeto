using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 500;
    public GameObject deathEffect;
    public bool isInvulnerable = false;
    private bool isDead = false;
    public float removeEnemy = 1.3f; // D��man� kald�rma s�resi
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        anim.SetTrigger("Die");

        // Oyunu yava�latmak i�in Time.timeScale kullan�l�r
        Time.timeScale = 0.1f; // Yava�lama fakt�r�n� ayarlayabilirsiniz

        StartCoroutine(DisableBossAndResumeGame());
    }

    IEnumerator DisableBossAndResumeGame()
    {
        yield return new WaitForSeconds(0.8f); // Animasyonun s�resine g�re ayarlay�n

        // Oyunu yeniden normal h�zda �al��t�r
        Time.timeScale = 1f;

        // Boss objesini yok et
        Destroy(gameObject);

        // Opsiyonel olarak �l�m efekti olu�turabilirsiniz
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }
}
