using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneGecisi : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTriggered)
        {
            virtualCamera.Follow = null; // CinemachineVirtualCamera'n�n follow de�eri None olacak
            isTriggered = true;
            Invoke("ChangeScene", 2f); // 2 saniye sonra sahne de�i�ecek
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Ana men� sahnesine ge�
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

