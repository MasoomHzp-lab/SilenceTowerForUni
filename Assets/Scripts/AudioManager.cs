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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // اینجا تعیین می‌کنیم تو هر صحنه چه BGM پلی بشه
        AudioClip target = GetBgmForScene(scene.name);
        PlayBGM(target);
    }

    private AudioClip GetBgmForScene(string sceneName)
    {
        // اسم صحنه‌ها رو دقیقاً مطابق پروژه‌ت گذاشتم:
        // FirstPage, Level1, Level2, Level3
        switch (sceneName)
        {
            case "FirstPage": return menuBGM;
            case "Level1": return level1BGM;
            case "Level2": return level2BGM;
            case "Level3": return level3BGM;
            default: return menuBGM; // اگر صحنه جدید اضافه شد
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

    // برای راحتی:
    public void SFX_Star() => PlaySFX(starSFX);
    public void SFX_Bomb() => PlaySFX(bombSFX);
    public void SFX_Win()  => PlaySFX(winSFX);
    public void SFX_Lose() => PlaySFX(loseSFX);
}
