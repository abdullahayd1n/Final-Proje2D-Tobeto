using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 500;
    public GameObject deathEffect;
    public bool isInvulnerable = false;
    private bool isDead = false;
    public float removeEnemy = 1.3f; // Düþmaný kaldýrma süresi
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

        // Oyunu yavaþlatmak için Time.timeScale kullanýlýr
        Time.timeScale = 0.1f; // Yavaþlama faktörünü ayarlayabilirsiniz

        StartCoroutine(DisableBossAndResumeGame());
    }

    IEnumerator DisableBossAndResumeGame()
    {
        yield return new WaitForSeconds(0.8f); // Animasyonun süresine göre ayarlayýn

        // Oyunu yeniden normal hýzda çalýþtýr
        Time.timeScale = 1f;

        // Boss objesini yok et
        Destroy(gameObject);

        // Opsiyonel olarak ölüm efekti oluþturabilirsiniz
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }
}
