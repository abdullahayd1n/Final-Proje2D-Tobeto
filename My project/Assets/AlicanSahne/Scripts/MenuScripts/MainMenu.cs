using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
   {
    MainMenuSoundManager.Instance.PlaySFX("Play");
    SceneManager.LoadSceneAsync(1);
   }
   public void QuitGame()
   {
    MainMenuSoundManager.Instance.PlaySFX("Quit");
    Application.Quit();
    Debug.Log("oyun bitti");
   }
    public void GoToMainMenu()
    {

        SceneManager.LoadSceneAsync(0); // Ana men� sahnesine ge�
    }

}
