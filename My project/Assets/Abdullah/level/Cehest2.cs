using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cehest2 : MonoBehaviour
{
    public Animator chestAnimator; // Sand�k animator bile�eni
    private bool isPlayerInside; // Karakterin sand�k i�inde olup olmad���n� belirten boolean
    public GameObject button; // A�ma d��mesi
    public Canvas konusmaBalonu;

    public GameObject[] objectsToSpawn; // Sand�ktan ��kacak objelerin listesi
    private int maxObjectsToSpawn = 3; // Sand�ktan ��kacak maksimum obje say�s�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true; // Karakter sand�k i�ine girdi�inde boolean de�eri true olarak ayarla
            button.SetActive(true);
            KonusmaBalonuKanvasAc();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false; // Karakter sand�k i�inden ��kt���nda boolean de�eri false olarak ayarla
            button.SetActive(false);
            KonusmaBalonuKapat();
        }
    }

    private void Update()
    {
        // E tu�una bas�ld���nda ve karakter sand�k i�indeyken
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest(); // Sand��� a�
        }
    }

    public void OpenChestOnClick()
    {
        OpenChest(); // D��meye t�kland���nda sand��� a�
    }

    private void OpenChest()
    {
        chestAnimator.SetBool("isOpen", true); // Sand�k animasyonunun "isOpen" parametresini true yaparak a��lmas�n� sa�la

        // Sand�ktan ��kacak objeleri se�mek i�in bir k�me (HashSet) olu�tur
        HashSet<GameObject> selectedObjects = new HashSet<GameObject>();

        // Belirtilen maksimum say�da obje se�ilene kadar d�ng�y� devam ettir
        while (selectedObjects.Count < maxObjectsToSpawn)
        {
            // Rastgele bir obje se� ve k�meye ekle
            GameObject selectedObject = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
            selectedObjects.Add(selectedObject);
        }

        // Se�ilen objeleri olu�turarak sahneye ekleyin
        foreach (GameObject obj in selectedObjects)
        {
            Instantiate(obj, transform.position, Quaternion.identity);
        }

        // 1 saniye sonra sand���n yok olmas�n� sa�la
        Invoke("DestroyChest", 1f);
    }

    private void KonusmaBalonuKanvasAc()
    {
        if(konusmaBalonu!=null)
            konusmaBalonu.gameObject.SetActive(true);
    }

    private void KonusmaBalonuKapat()
    {
        if (konusmaBalonu != null)
            konusmaBalonu.gameObject.SetActive(false);
    }

    private void DestroyChest()
    {
        Destroy(gameObject); // Sand�k objesini yok et
    }
}
