using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraScript : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera mainCamera;
    public Cinemachine.CinemachineVirtualCamera cinematicCamera;
    public Canvas selectedCanvas;
    public GameObject gameobj;

    private bool isSwitched = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isSwitched)
        {
            isSwitched = true;
            SwitchCamera();
            Invoke("ReturnToMainCamera", 3f); // 3 saniye sonra geri dön
            Invoke("DisableCanvas", 0f); // Kamerayý deðiþtirirken Canvas'ý hemen kapat
            Invoke("EnableCanvas", 5f); // 5 saniye sonra Canvas'ý tekrar aç
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