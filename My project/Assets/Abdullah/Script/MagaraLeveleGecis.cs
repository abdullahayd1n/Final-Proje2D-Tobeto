using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagaraLeveleGecis : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Canvas[] canvasesToDisable; // Kapamak istedi�iniz Canvas'leri buradan s�r�kleyip b�rakabilirsiniz
    private bool isTriggered = false;
    private Rigidbody2D playerRigidbody; // Oyuncu nesnesinin Rigidbody bile�eni
    private float moveSpeed = 5f; // Oyuncunun hareket h�z�
    private float moveDirection = 1f; // Hareket y�n� (sa�a do�ru)
    private float moveDuration = 5f; // Hareket s�resi

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            // Oyuncu collider'�ndan ge�tikten sonra karakterin hareketini ba�lat
            MovePlayer();

            // Belirli bir s�re sonra karakterin hareketini durdur
            Invoke("StopPlayerMovement", moveDuration);

            // Canvas'leri kapat
            DisableCanvases();

            // Sahne de�i�imini ba�lat
            Invoke("ChangeScene", moveDuration + 1f); // Hareket s�resine 1 saniye ekleyerek sahne de�i�imini geciktiriyoruz
        }
    }

    private void DisableCanvases()
    {
        // T�m belirtilen Canvas'leri etkisiz hale getir
        foreach (Canvas canvas in canvasesToDisable)
        {
            canvas.enabled = false;
        }
    }

    private void Start()
    {
        // Oyuncu nesnesinin Rigidbody bile�enini al�yoruz
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void MovePlayer()
    {
        // Oyuncuyu belirtilen y�nde belirtilen h�zda hareket ettir
        playerRigidbody.velocity = new Vector2(moveSpeed * moveDirection, playerRigidbody.velocity.y);
    }

    private void StopPlayerMovement()
    {
        // Oyuncunun hareketini durdur
        playerRigidbody.velocity = Vector2.zero;
    }

    private void ChangeScene()
    {
        // Bir sonraki sahneye ge�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnDestroy()
    {
        // Bu nesne yok edildi�inde �al��acak temizleme i�lemleri
        // �rne�in: Invoke i�lemlerini iptal etmek
        CancelInvoke();
    }
}

