using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandikControl : MonoBehaviour
{
    public Animator chestAnimator; // Sand�k animator bile�eni

    private bool isPlayerInside; // Karakterin sand�k i�inde olup olmad���n� belirten boolean

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true; // Karakter sand�k i�ine girdi�inde boolean de�eri true olarak ayarla
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false; // Karakter sand�k i�inden ��kt���nda boolean de�eri false olarak ayarla
        }
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E)) // E tu�una bas�ld���nda ve karakter sand�k i�indeyken
        {
            chestAnimator.SetBool("isOpen", true); // Sand�k animasyonunun "isOpen" parametresini true yaparak a��lmas�n� sa�la
        }
    }
}


