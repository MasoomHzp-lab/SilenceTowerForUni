using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject pausePanel;      // پنل پاز
    public Button muteButton;          // دکمه میوت
    public Sprite muteOnIcon;          // (اختیاری) آیکن وقتی میوت هست
    public Sprite muteOffIcon;         // (اختیاری) آیکن وقتی میوت نیست
    public Image muteButtonImage;      // (اختیاری) تصویر دکمه

    bool isPaused;

    void Start()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        isPaused = false;
        UpdateMuteUI();
    }

    void Update()
    {
        // اگر با ESC میخوای باز/بسته شه:
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        if (pausePanel != null) pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("FirstPage"); // اسم منو تو پروژه‌ت
    }

    // ✅ فقط BGM میوت میشه (نه SFX)
    public void ToggleMuteBGM()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.ToggleBgmMute();

        UpdateMuteUI();
    }

    void UpdateMuteUI()
    {
        if (AudioManager.Instance == null) return;

        if (muteButtonImage != null && muteOnIcon != null && muteOffIcon != null)
        {
            muteButtonImage.sprite = AudioManager.Instance.IsBgmMuted ? muteOnIcon : muteOffIcon;
        }

        // اگر به جای آیکن، متن دکمه داری:
        // var txt = muteButton.GetComponentInChildren<TMPro.TMP_Text>();
        // if (txt != null) txt.text = AudioManager.Instance.IsBgmMuted ? "Unmute Music" : "Mute Music";
    }
}
