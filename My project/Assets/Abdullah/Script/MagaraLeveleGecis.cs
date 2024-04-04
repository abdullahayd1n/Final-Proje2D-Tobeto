using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagaraLeveleGecis : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Canvas[] canvasesToDisable; // Kapamak istediðiniz Canvas'leri buradan sürükleyip býrakabilirsiniz
    private bool isTriggered = false;
    private Rigidbody2D playerRigidbody; // Oyuncu nesnesinin Rigidbody bileþeni
    private float moveSpeed = 5f; // Oyuncunun hareket hýzý
    private float moveDirection = 1f; // Hareket yönü (saða doðru)
    private float moveDuration = 5f; // Hareket süresi

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            // Oyuncu collider'ýndan geçtikten sonra karakterin hareketini baþlat
            MovePlayer();

            // Belirli bir süre sonra karakterin hareketini durdur
            Invoke("StopPlayerMovement", moveDuration);

            // Canvas'leri kapat
            DisableCanvases();

            // Sahne deðiþimini baþlat
            Invoke("ChangeScene", moveDuration + 1f); // Hareket süresine 1 saniye ekleyerek sahne deðiþimini geciktiriyoruz
        }
    }

    private void DisableCanvases()
    {
        // Tüm belirtilen Canvas'leri etkisiz hale getir
        foreach (Canvas canvas in canvasesToDisable)
        {
            canvas.enabled = false;
        }
    }

    private void Start()
    {
        // Oyuncu nesnesinin Rigidbody bileþenini alýyoruz
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void MovePlayer()
    {
        // Oyuncuyu belirtilen yönde belirtilen hýzda hareket ettir
        playerRigidbody.velocity = new Vector2(moveSpeed * moveDirection, playerRigidbody.velocity.y);
    }

    private void StopPlayerMovement()
    {
        // Oyuncunun hareketini durdur
        playerRigidbody.velocity = Vector2.zero;
    }

    private void ChangeScene()
    {
        // Bir sonraki sahneye geç
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnDestroy()
    {
        // Bu nesne yok edildiðinde çalýþacak temizleme iþlemleri
        // Örneðin: Invoke iþlemlerini iptal etmek
        CancelInvoke();
    }
}

