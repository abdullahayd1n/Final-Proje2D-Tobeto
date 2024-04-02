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
            virtualCamera.Follow = null; // CinemachineVirtualCamera'nýn follow deðeri None olacak
            isTriggered = true;
            Invoke("ChangeScene", 2f); // 2 saniye sonra sahne deðiþecek
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Ana menü sahnesine geç
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

