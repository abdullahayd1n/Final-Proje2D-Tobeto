using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandikControl : MonoBehaviour
{
    public Animator chestAnimator; // Sandýk animator bileþeni
    private bool isPlayerInside; // Karakterin sandýk içinde olup olmadýðýný belirten boolean
    public GameObject button; // Açma düðmesi

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true; // Karakter sandýk içine girdiðinde boolean deðeri true olarak ayarla
            button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false; // Karakter sandýk içinden çýktýðýnda boolean deðeri false olarak ayarla
            button.SetActive(false);
        }
    }

    private void Update()
    {
        // E tuþuna basýldýðýnda ve karakter sandýk içindeyken
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest(); // Sandýðý aç
        }
    }

    public void OpenChestOnClick()
    {
        OpenChest(); // Düðmeye týklandýðýnda sandýðý aç
    }

    private void OpenChest()
    {
        chestAnimator.SetBool("isOpen", true); // Sandýk animasyonunun "isOpen" parametresini true yaparak açýlmasýný saðla
    }
}

