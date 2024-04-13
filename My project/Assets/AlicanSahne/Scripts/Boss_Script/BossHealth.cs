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
    public float removeEnemy = 2f; // Düþmaný kaldýrma süresi
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
        Time.timeScale = 0.5f; // Yavaþlama faktörünü ayarlayabilirsiniz

        StartCoroutine(DisableBossAndResumeGame());

        Destroy(tas1.gameObject);
        Destroy(tas2.gameObject);
        Invoke("SwitchCamera", 1.5f);

    }
    private void SwitchCamera()
    {
        PlayerCam.Priority = 20; // Ana kamerayý düþük öncelikle ac
        bosCamera.Priority = 10; // Cinematic kamerayý yüksek öncelikle kapat
    }

    IEnumerator DisableBossAndResumeGame()
    {
        yield return new WaitForSeconds(1.5f); // Animasyonun süresine göre ayarlayýn

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
