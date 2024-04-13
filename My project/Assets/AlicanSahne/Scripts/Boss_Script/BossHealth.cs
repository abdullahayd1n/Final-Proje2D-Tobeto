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
    public float removeEnemy = 1.3f; // Düþmaný kaldýrma süresi
    private Animator anim;
    

    // Ölüm sonrasý atýlacak para prefabý
    public GameObject moneyPrefab;
    public int moneyAmount = 21;

    // Item drop prefab'larý
    public GameObject[] itemDropPrefabs;

    // Düþüþ sayacý
    private int dropCounter = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;

        // Her %25'lik dilimde bir kez düþürme kontrolü
        if (health <= 375 && dropCounter == 0)
        {
            DropItem();
            dropCounter++;
        }
        else if (health <= 250 && dropCounter == 1)
        {
            DropItem();
            dropCounter++;
        }
        else if (health <= 125 && dropCounter == 2)
        {
            DropItem();
            dropCounter++;
        }
        else if (health <= 0 && dropCounter == 3)
        {
            DropItem();
            dropCounter++;
        }

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

        // Ölüm sonrasý paralarýn atýlmasý
        for (int i = 0; i < moneyAmount; i++)
        {
            Instantiate(moneyPrefab, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
        }

        StartCoroutine(DisableBossAndResumeGame());

        Destroy(tas1.gameObject);
        Destroy(tas2.gameObject);
        SwitchCamera();

    }
    private void SwitchCamera()
    {
        PlayerCam.Priority = 20; // Ana kamerayý düþük öncelikle ac
        bosCamera.Priority = 10; // Cinematic kamerayý yüksek öncelikle kapat
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

    // Rastgele bir öðe düþürme fonksiyonu
    private void DropItem()
    {
        if (itemDropPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, itemDropPrefabs.Length); // Rastgele bir index seçer.
            Instantiate(itemDropPrefabs[randomIndex], transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
}