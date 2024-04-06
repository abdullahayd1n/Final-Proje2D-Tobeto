using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenNot : MonoBehaviour
{
    public GameObject notObject;

    public AudioSource audioSource;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&& notObject != null)
        {
            notObject.SetActive(true);
            audioSource.Play();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")&& notObject != null)
        {
            notObject.SetActive(false);
        }
    }
}

