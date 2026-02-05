using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("BGM Clips")]
    public AudioClip menuBGM;
    public AudioClip level1BGM;
    public AudioClip level2BGM;
    public AudioClip level3BGM;

    [Header("SFX Clips")]
    public AudioClip starSFX;
    public AudioClip bombSFX;   // یا همون کاهش جون
    public AudioClip winSFX;
    public AudioClip loseSFX;

    private AudioClip currentBgm;

    // ✅ برای ذخیره وضعیت میوت موسیقی
    private const string BgmMuteKey = "BGM_MUTED";

    // ✅ برای اینکه PauseMenu بتونه بفهمه الان میوت هست یا نه
    public bool IsBgmMuted => bgmSource != null && bgmSource.mute;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // اگر از قبل ست نکردی
        if (bgmSource == null) bgmSource = gameObject.AddComponent<AudioSource>();
        if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();

        bgmSource.loop = true;
        bgmSource.playOnAwake = false;

        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.spatialBlend = 0f; // 2D

        // ✅ لود وضعیت میوت BGM
        bool muted = PlayerPrefs.GetInt(BgmMuteKey, 0) == 1;
        bgmSource.mute = muted;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip target = GetBgmForScene(scene.name);
        PlayBGM(target);
    }

    private AudioClip GetBgmForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "FirstPage": return menuBGM;
            case "Level1": return level1BGM;
            case "Level2": return level2BGM;
            case "Level3": return level3BGM;
            default: return menuBGM;
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (clip == null) return;
        if (currentBgm == clip && bgmSource.isPlaying) return;

        currentBgm = clip;
        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
        currentBgm = null;
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }

    // ✅ فقط BGM رو میوت/آن‌میوت کن (SFX دست نخورده)
    public void ToggleBgmMute()
    {
        if (bgmSource == null) return;
        bgmSource.mute = !bgmSource.mute;

        PlayerPrefs.SetInt(BgmMuteKey, bgmSource.mute ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetBgmMute(bool mute)
    {
        if (bgmSource == null) return;
        bgmSource.mute = mute;

        PlayerPrefs.SetInt(BgmMuteKey, mute ? 1 : 0);
        PlayerPrefs.Save();
    }

    // برای راحتی:
    public void SFX_Star() => PlaySFX(starSFX);
    public void SFX_Bomb() => PlaySFX(bombSFX);
    public void SFX_Win()  => PlaySFX(winSFX);
    public void SFX_Lose() => PlaySFX(loseSFX);
}
