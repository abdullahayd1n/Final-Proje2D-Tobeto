using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KameraScript : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera mainCamera;
    public Cinemachine.CinemachineVirtualCamera cinematicCamera;
    public Canvas selectedCanvas;
    public Canvas konusmaBalonu;
    public GameObject gameobj;

    private bool isSwitched = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isSwitched)
        {
            isSwitched = true;
            SwitchCamera();
            Invoke("ReturnToMainCamera", 3f); // 3 saniye sonra geri dön
            //---------------------------------------------------------------------
            Invoke("DisableCanvas", 0f); // Kamerayý deðiþtirirken Canvas'ý hemen kapat
            Invoke("EnableCanvas", 5f); // 5 saniye sonra Canvas'ý tekrar aç
            //-----------------------------------------------------------------------------
            Invoke("KonusmaBalCanvasAc", 5f); // 5 saniye sonra KonusmaCanvas'ý tekrar aç
            Invoke("KonusmaBalCanvasKapat", 8f); // Kamerayý deðiþtirirken Canvas'ý hemen kapat

            EnableGameObjekt();

        }
    }

    private void SwitchCamera()
    {
        mainCamera.Priority = 10; // Ana kamerayý düþük öncelikle kapat
        cinematicCamera.Priority = 20; // Cinematic kamerayý yüksek öncelikle aç
    }

    private void ReturnToMainCamera()
    {
        mainCamera.Priority = 20; // Ana kamerayý yüksek öncelikle aç
        cinematicCamera.Priority = 10; // Cinematic kamerayý düþük öncelikle kapat
        isSwitched = false;
    }

    private void KonusmaBalCanvasAc()
    {
        if (konusmaBalonu != null)
            konusmaBalonu.gameObject.SetActive(true);
    }
    private void KonusmaBalCanvasKapat()
    {
        if (konusmaBalonu != null)
            konusmaBalonu.gameObject.SetActive(false);
    }

    private void DisableCanvas()
    {
        if (selectedCanvas != null)
            selectedCanvas.enabled = false;
    }

    private void EnableCanvas()
    {
        if (selectedCanvas != null)
            selectedCanvas.enabled = true;
    }
    private void EnableGameObjekt()
    {
        if(gameobj!=null)
            gameobj.SetActive(false);
    }
}