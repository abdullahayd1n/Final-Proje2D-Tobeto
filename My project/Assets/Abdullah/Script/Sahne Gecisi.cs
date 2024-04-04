using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneGecisi : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Canvas[] canvasesToDisable; // Kapamak istediðiniz Canvas'leri buradan sürükleyip býrakabilirsiniz
    private bool isTriggered = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTriggered)
        {
            virtualCamera.Follow = null; // CinemachineVirtualCamera'nýn follow deðeri None olacak
            isTriggered = true;
            // Canvas'leri kapat
            DisableCanvases();

            // Sahne deðiþimini baþlat
            Invoke("ChangeScene", +3f); // Hareket süresine 3 saniye ekleyerek sahne deðiþimini geciktiriyoruz
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

