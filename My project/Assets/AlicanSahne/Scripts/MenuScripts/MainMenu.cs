using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
    public void GoToMainMenu()
    {

        SceneManager.LoadSceneAsync("MainMenu"); // Ana men� sahnesine ge�
    }

}
