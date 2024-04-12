using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cehest2 : MonoBehaviour
{
    public Animator chestAnimator; // Sandýk animator bileþeni
    private bool isPlayerInside; // Karakterin sandýk içinde olup olmadýðýný belirten boolean
    public GameObject button; // Açma düðmesi
    public Canvas konusmaBalonu;

    public GameObject[] objectsToSpawn; // Sandýktan çýkacak objelerin listesi
    private int maxObjectsToSpawn = 3; // Sandýktan çýkacak maksimum obje sayýsý

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true; // Karakter sandýk içine girdiðinde boolean deðeri true olarak ayarla
            button.SetActive(true);
            KonusmaBalonuKanvasAc();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false; // Karakter sandýk içinden çýktýðýnda boolean deðeri false olarak ayarla
            button.SetActive(false);
            KonusmaBalonuKapat();
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

        // Sandýktan çýkacak objeleri seçmek için bir küme (HashSet) oluþtur
        HashSet<GameObject> selectedObjects = new HashSet<GameObject>();

        // Belirtilen maksimum sayýda obje seçilene kadar döngüyü devam ettir
        while (selectedObjects.Count < maxObjectsToSpawn)
        {
            // Rastgele bir obje seç ve kümeye ekle
            GameObject selectedObject = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
            selectedObjects.Add(selectedObject);
        }

        // Seçilen objeleri oluþturarak sahneye ekleyin
        foreach (GameObject obj in selectedObjects)
        {
            Instantiate(obj, transform.position, Quaternion.identity);
        }

        // 1 saniye sonra sandýðýn yok olmasýný saðla
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
        Destroy(gameObject); // Sandýk objesini yok et
    }
}
