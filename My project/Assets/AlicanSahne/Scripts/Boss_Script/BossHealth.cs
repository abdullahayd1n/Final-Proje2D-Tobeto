using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public GameObject tas1;
    public GameObject tas2;
    public Cinemachine.CinemachineVirtualCamera PlayerCam;
    public Cinemachine.CinemachineVirtualCamera bosCamera;

    public int health = 500;
    public GameObject deathEffect;
    public bool isInvulnerable = false;
    private bool isDead = false;
    public float removeEnemy = 2f; // D��man� kald�rma s�resi
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
        Time.timeScale = 0.5f; // Yava�lama fakt�r�n� ayarlayabilirsiniz

        StartCoroutine(DisableBossAndResumeGame());

        Destroy(tas1.gameObject);
        Destroy(tas2.gameObject);
        Invoke("SwitchCamera", 1.5f);

    }
    private void SwitchCamera()
    {
        PlayerCam.Priority = 20; // Ana kameray� d���k �ncelikle ac
        bosCamera.Priority = 10; // Cinematic kameray� y�ksek �ncelikle kapat
    }

    IEnumerator DisableBossAndResumeGame()
    {
        yield return new WaitForSeconds(1.5f); // Animasyonun s�resine g�re ayarlay�n

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
