using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManageer : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject gameOverPanel;
    public GameObject winPanel;

    public void GameOver()
    {
        Time.timeScale = 0f; // متوقف کردن بازی
        gameOverPanel.SetActive(true);
    }

    public void Win()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NextLevel(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
