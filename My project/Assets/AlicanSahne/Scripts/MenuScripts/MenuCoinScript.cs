using UnityEngine;
using UnityEngine.UI;

public class MenuCoinScript : MonoBehaviour
{
    public Text coinText; // Alt�n say�s�n� g�sterecek metin alan�

    void Start()
    {
        int collectedCoins = PlayerPrefs.GetInt("ButonLucas_Coins", 0);
        coinText.text = " " + collectedCoins.ToString();
    }

}
