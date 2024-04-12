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

    public GameObject seviyepanelObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTriggered)
        {
            virtualCamera.Follow = null;
            isTriggered = true;
            
            // Canvas'leri kapat
            DisableCanvases();

            ChangeScene();

            // Sahne de�i�imini ba�lat
            //Invoke("ChangeScene",+ 1.5f); // Hareket s�resine 1 saniye ekleyerek sahne de�i�imini geciktiriyoruz
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
    private void ChangeScene()
    {
        // Bir sonraki sahneye ge�
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        seviyepanelObject.SetActive(true);
    }

    private void OnDestroy()
    {
        // Bu nesne yok edildi�inde �al��acak temizleme i�lemleri
        // �rne�in: Invoke i�lemlerini iptal etmek
        CancelInvoke();
    }
}

