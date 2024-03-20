using UnityEngine;
using UnityEngine.UI;

public class YadigarCoinScriptt : MonoBehaviour
{
    public Text yadigarText; // Alt�n say�s�n� g�sterecek metin alan�

    void Start()
    {
        // Oyuncunun toplam yadigar say�s�n� PlayerPrefs'ten al
        int collectedYadigar = PlayerPrefs.GetInt("ButonLucas_Yadigar", 0);
        // Metin alan�nda toplanan yadigar say�s�n� g�ster
        yadigarText.text = " " + collectedYadigar.ToString();
    }
}
