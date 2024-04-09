using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMute : MonoBehaviour
{
    public AudioSource musicSource; // AudioSource bileşeni

    void Start()
    {
        // musicSource değişkenini sahnede bulunan AudioSource bileşeniyle bağlayın
        musicSource = GetComponent<AudioSource>();
    }

    public void MuteHandler(bool mute)
    {
        // "mute" değerine göre müziği açıp kapatın
        if (mute)
        {
            musicSource.volume = 0; // Ses seviyesini 0 olarak ayarlayarak sesi kapatın
        }
        else
        {
            musicSource.volume = 1; // Ses seviyesini 1 olarak ayarlayarak sesi açın
        }
    }
}
