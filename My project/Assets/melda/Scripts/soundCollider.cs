using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundCollider : MonoBehaviour
{
    public AudioSource sesDosyasi; // Oynatılacak ses dosyası

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Karakter bu ses alanına girdiğinde
        if (other.CompareTag("Karakter"))
        {
            // Ses dosyasını başlat
            sesDosyasi.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Karakter bu ses alanından çıktığında
        if (other.CompareTag("Karakter"))
        {
            // Ses dosyasını durdur
            sesDosyasi.Stop();
        }
    }
}
