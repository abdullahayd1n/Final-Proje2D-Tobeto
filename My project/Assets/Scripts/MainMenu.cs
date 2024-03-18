using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
public class MainMenuS : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer audioMixer;



    public void SetVolume()


    {
        audioMixer.SetFloat("volume", volumeSlider.value);
        
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
