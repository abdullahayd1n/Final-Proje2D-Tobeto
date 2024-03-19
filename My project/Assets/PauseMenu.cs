// PauseMenu.cs

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settings;
    private bool isPaused = false;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    // Player �ld���nde �a�r�lacak metod
    public void PlayerDied()
    {
        Pause(); // Pause Menu'yi a�
        isPaused = true; // Oyunun durdu�unu belirt
    }

    public void Settings()
    {
        pauseMenu.SetActive(false);
        settings.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
}
