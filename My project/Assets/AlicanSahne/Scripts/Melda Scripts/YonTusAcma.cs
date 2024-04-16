using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YonTusAcma : MonoBehaviour
{
    public GameObject tusObject;

    public AudioSource audioSource;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&& tusObject != null)
        {
            tusObject.SetActive(true);
            audioSource.Play();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")&& tusObject != null)
        {
            tusObject.SetActive(false);
        }
    }
}
