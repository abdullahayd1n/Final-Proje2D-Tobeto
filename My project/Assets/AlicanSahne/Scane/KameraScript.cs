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
            Invoke("ReturnToMainCamera", 3f); // 3 saniye sonra geri d�n
            Invoke("DisableCanvas", 0f); // Kameray� de�i�tirirken Canvas'� hemen kapat
            Invoke("EnableCanvas", 5f); // 5 saniye sonra Canvas'� tekrar a�
            EnableGameObjekt();
        }
    }

    private void SwitchCamera()
    {
        mainCamera.Priority = 10; // Ana kameray� d���k �ncelikle kapat
        cinematicCamera.Priority = 20; // Cinematic kameray� y�ksek �ncelikle a�
    }

    private void ReturnToMainCamera()
    {
        mainCamera.Priority = 20; // Ana kameray� y�ksek �ncelikle a�
        cinematicCamera.Priority = 10; // Cinematic kameray� d���k �ncelikle kapat
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