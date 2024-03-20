using UnityEngine;
using UnityEngine.UI;

public class YadigarCoinScriptt : MonoBehaviour
{
    public Text  yadigarText; // Altýn sayýsýný gösterecek metin alaný

    void Start()
    {
        int collectedYadigar = PlayerPrefs.GetInt("ButonLucas_Yadigar", 0);
        yadigarText.text = " " + collectedYadigar.ToString();
    }

}
