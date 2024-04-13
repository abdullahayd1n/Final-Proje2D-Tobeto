using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosGris : MonoBehaviour
{
    public GameObject bostutumu;
    public Cinemachine.CinemachineVirtualCamera PlayerCam;
    public Cinemachine.CinemachineVirtualCamera bosCamera;
    public Rigidbody2D rb;
    public Rigidbody2D rb1;

    private float fallDelay = 0.001f;

    private bool isSwitched = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TriggeredFall());
        }

        isSwitched = true;
        SwitchCamera();

        Destroy(bostutumu.gameObject);
    }

    private void SwitchCamera()
    {
        PlayerCam.Priority = 10; // Ana kamerayý düþük öncelikle kapat
        bosCamera.Priority = 20; // Cinematic kamerayý yüksek öncelikle aç
    }

    private IEnumerator TriggeredFall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb1.bodyType = RigidbodyType2D.Dynamic;
    }
}