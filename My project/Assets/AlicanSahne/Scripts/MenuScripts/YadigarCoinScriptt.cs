using UnityEngine;
using UnityEngine.UI;

public class YadigarCoinScriptt : MonoBehaviour
{
    public Text yadigarText; // Altýn sayýsýný gösterecek metin alaný

    void Start()
    {
        // Oyuncunun toplam yadigar sayýsýný PlayerPrefs'ten al
        int collectedYadigar = PlayerPrefs.GetInt("ButonLucas_Yadigar", 0);
        // Metin alanýnda toplanan yadigar sayýsýný göster
        yadigarText.text = " " + collectedYadigar.ToString();
    }
}
