using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandikControl : MonoBehaviour
{
    public Animator chestAnimator; // Sandýk animator bileþeni

    private bool isPlayerInside; // Karakterin sandýk içinde olup olmadýðýný belirten boolean

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true; // Karakter sandýk içine girdiðinde boolean deðeri true olarak ayarla
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false; // Karakter sandýk içinden çýktýðýnda boolean deðeri false olarak ayarla
        }
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E)) // E tuþuna basýldýðýnda ve karakter sandýk içindeyken
        {
            chestAnimator.SetBool("isOpen", true); // Sandýk animasyonunun "isOpen" parametresini true yaparak açýlmasýný saðla
        }
    }
}


