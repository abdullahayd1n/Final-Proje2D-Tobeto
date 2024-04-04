using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settings;
    private bool isPaused = false;
    public LucasHealth lucasHealth;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

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
        // Checkpoint konumunu al
        Vector3 respawnPosition = Vector3.zero; // Varsayýlan bir konum

        // Karakterin respawn noktasýna geri döndür
        lucasHealth.RespawnAtPosition(respawnPosition);

        // Sahneyi yeniden yükle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Zaman ölçeðini varsayýlan deðere geri getir
        Time.timeScale = 1;
    }

    // Player öldüðünde çaðrýlacak metod
    public void PlayerDied()
    {
        Pause(); // Pause Menu'yi aç
        isPaused = true; // Oyunun durduðunu belirt
    }

    public void Settings()
    {
        pauseMenu.SetActive(false);
        settings.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
}