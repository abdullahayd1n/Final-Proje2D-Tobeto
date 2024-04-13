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
    public float removeEnemy = 1.3f; // D��man� kald�rma s�resi
    private Animator anim;
    

    // �l�m sonras� at�lacak para prefab�
    public GameObject moneyPrefab;
    public int moneyAmount = 21;

    // Item drop prefab'lar�
    public GameObject[] itemDropPrefabs;

    // D���� sayac�
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

        // Her %25'lik dilimde bir kez d���rme kontrol�
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

        // Oyunu yava�latmak i�in Time.timeScale kullan�l�r
        Time.timeScale = 0.5f; // Yava�lama fakt�r�n� ayarlayabilirsiniz

        // �l�m sonras� paralar�n at�lmas�
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
        PlayerCam.Priority = 20; // Ana kameray� d���k �ncelikle ac
        bosCamera.Priority = 10; // Cinematic kameray� y�ksek �ncelikle kapat
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

    // Rastgele bir ��e d���rme fonksiyonu
    private void DropItem()
    {
        if (itemDropPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, itemDropPrefabs.Length); // Rastgele bir index se�er.
            Instantiate(itemDropPrefabs[randomIndex], transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
}