using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
    public AudioClip musicClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = musicClip;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("watersound"))
        {
            audioSource.Play();
            Debug.Log("jsdjsk");
        }
    }
}
