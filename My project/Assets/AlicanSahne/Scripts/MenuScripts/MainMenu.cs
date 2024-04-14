using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{
    public GameObject objectToToggle;

    public void ToggleObject()
    {
        objectToToggle.SetActive(!objectToToggle.activeSelf);
    }

    public void PlayGame()
   {
    MainMenuSoundManager.Instance.PlaySFX("Play");
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
   public void QuitGame()
   {
    MainMenuSoundManager.Instance.PlaySFX("Quit");
    Application.Quit();
    Debug.Log("oyun bitti");
   }

    


}
