using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeviyeBitirme : MonoBehaviour
{
    public void seviye1bitir()
    {
        SeviyeYonetici.seviye2 = true;
        SceneManager.LoadScene("Seviyeler");
    }

    public void seviye2bitir()
    {
        SeviyeYonetici.seviye3 = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
}
