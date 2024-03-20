using UnityEngine;
using UnityEngine.UI;

public class MenuCoinScript : MonoBehaviour
{
    public Text coinText; // Alt�n say�s�n� g�sterecek metin alan�

    void Start()
    {
        // PlayerPrefs'tan toplanan alt�n say�s�n� al ve metin alan�na ata
        int collectedCoins = PlayerPrefs.GetInt("ButonLucas", 0);
        coinText.text = " " + collectedCoins.ToString();
    }
}
