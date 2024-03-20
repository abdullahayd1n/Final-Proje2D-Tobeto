using UnityEngine;
using UnityEngine.UI;

public class MenuCoinScript : MonoBehaviour
{
    public Text coinText; // Altýn sayýsýný gösterecek metin alaný

    void Start()
    {
        // PlayerPrefs'tan toplanan altýn sayýsýný al ve metin alanýna ata
        int collectedCoins = PlayerPrefs.GetInt("ButonLucas", 0);
        coinText.text = " " + collectedCoins.ToString();
    }
}
