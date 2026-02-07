using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject pausePanel;

    [Header("Mute Button Text")]
    public TMP_Text muteButtonText; // متن دکمه Mute / Unmute

    bool isPaused;

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        isPaused = false;
        UpdateMuteText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        isPaused = false;

        Time.timeScale = 1f;
        SceneManager.LoadScene("FirstPage");
    }


    // 🔊 فقط BGM میوت / آن‌میوت میشه
    public void ToggleMuteBGM()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.ToggleBgmMute();

        UpdateMuteText();
    }

    void UpdateMuteText()
    {
        if (muteButtonText == null || AudioManager.Instance == null) return;

        muteButtonText.text = AudioManager.Instance.IsBgmMuted
            ? "Unmute"
            : "Mute";
    }
}
