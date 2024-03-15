using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
public static bool gameIsPaused;
public GameObject pauseMenu;
public GameObject optionsMenu;
public GameObject panel;

void Update()
{
    if(Input.GetKeyDown(KeyCode.Escape))

    {
        if(gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();

        }
    }
}
public void Resume()
{
    pauseMenu.SetActive(false);
    panel.SetActive(false);
    Time.timeScale = 1f;
    gameIsPaused = false;
}
public void Pause()
{
    pauseMenu.SetActive(true);
    panel.SetActive(true);
    Time.timeScale = 0f;
    gameIsPaused = true; 
}
}
