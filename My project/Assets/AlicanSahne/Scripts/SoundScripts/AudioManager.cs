using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSound;
    public AudioSource musicSource, sfxSoucse;

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

    public void PauseResumeButtonSound()
    {
        AudioManager.Instance.PlaySFX("PauseResume");
    }

    public void PauseOptionsButtonSound()
    {
        AudioManager.Instance.PlaySFX("PauseOptions");

    }

    public void PauseMainMenuButtonSound()
    {
        AudioManager.Instance.PlaySFX("PauseMainMenu");
        Debug.Log("PauseMenu");
    }
    public void PauseRestartButtonSound()
    {
        AudioManager.Instance.PlaySFX("PauseRestart");
    }

    public void DeadMainMenuButtonSound()
    {
        AudioManager.Instance.PlaySFX("DeadMainMenu");
    }
    public void DeadRestartButtonSound()
    {
        AudioManager.Instance.PlaySFX("DeadRestart");
    
    }

    public void PanelCancelButtonSound()
    {
        AudioManager.Instance.PlaySFX("PanelCancel");
    }
    public void PanelFullScreenButtonSound()
    {
        AudioManager.Instance.PlaySFX("PanelFullScreen");
    }


}
