using UnityEngine;
using UnityEngine.UI;

public class YadigarCoinScriptt : MonoBehaviour
{
    public Text  yadigarText; // Alt�n say�s�n� g�sterecek metin alan�

    void Start()
    {
        int collectedYadigar = PlayerPrefs.GetInt("ButonLucas_Yadigar", 0);
        yadigarText.text = " " + collectedYadigar.ToString();
    }

}
