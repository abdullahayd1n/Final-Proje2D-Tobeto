using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuSesiCollider : MonoBehaviour
{
    public AudioSource audioSource;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
