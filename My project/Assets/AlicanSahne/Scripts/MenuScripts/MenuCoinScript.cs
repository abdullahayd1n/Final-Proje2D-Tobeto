using UnityEngine;
using UnityEngine.UI;

public class MenuCoinScript : MonoBehaviour
{
    public Text coinText; // Altýn sayýsýný gösterecek metin alaný

    void Start()
    {
        int collectedCoins = PlayerPrefs.GetInt("ButonLucas_Coins", 0);
        coinText.text = " " + collectedCoins.ToString();
    }

}
