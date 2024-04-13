using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MainMenuSoundManager : MonoBehaviour
{
    public static MainMenuSoundManager Instance;

    public Sound[] musicSounds, sfxSound;
    public AudioSource musicSource, sfxSoucse;

    public Slider _sfxSlider;

    public Slider _musicSlider;



    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            
        }
        
    }
    private void Start()
    {
        PlayMusic("Theme");
    }
    public void PlayMusic(string name)
    {
        Sound s=Array.Find(musicSounds,x=>x.name==name);

        if(s == null)
        {
            Debug.Log("Soun Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSound,x=>x.name==name);

        if(s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSoucse.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute=!musicSource.mute;
    }
    public void ToggleSFX()
    {
        sfxSoucse.mute=!sfxSoucse.mute;
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        sfxSoucse.volume = volume;
    }

    public void PlayButtonSound()
    {
        MainMenuSoundManager.Instance.PlaySFX("Options");
    }

    public void CancelButtonSound()
    {
        MainMenuSoundManager.Instance.PlaySFX("Cancel");

    }

    public void FullScreenButtonSound()
    {
        MainMenuSoundManager.Instance.PlaySFX("FullScreen");
    }

    // SFXVolume() metodunun adı değiştirildi ve artık sadece bir parametre alıyor
    public void SetSFXVolumeFromSlider(float volume)
    {
        sfxSoucse.volume = volume;
        Debug.Log("SFX volume set to: " + volume);
    }
    public void SetMusicVolumeFromSlider(float volume)
    {
        musicSource.volume = volume;
        
    }


[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
}
