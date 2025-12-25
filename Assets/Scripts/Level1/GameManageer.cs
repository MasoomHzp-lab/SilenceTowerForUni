using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManageer : MonoBehaviour
{
  public static GameManageer Instance;

    [Header("UI (Auto-found per scene)")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gameOverPanel;

    private const int MENU_INDEX = 0;
    private const int FIRST_LEVEL_INDEX = 1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void Start()
    {
        Time.timeScale = 1f;
        HookUIFromScene();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;
        HookUIFromScene();
    }

    private void HookUIFromScene()
    {
        winPanel = null;
        gameOverPanel = null;

        int idx = SceneManager.GetActiveScene().buildIndex;
        if (idx == MENU_INDEX) return;

        // ✅ پیدا کردن حتی اگر پنل‌ها Inactive باشند
        winPanel = FindInSceneByTagIncludingInactive("WinPanel");
        gameOverPanel = FindInSceneByTagIncludingInactive("LosePanel");

        // Fallback با اسم (اگر Tag نزدی)
        if (winPanel == null) winPanel = FindInSceneByNameIncludingInactive("winPanel");
        if (gameOverPanel == null) gameOverPanel = FindInSceneByNameIncludingInactive("LosePanel");

        // شروع لول: هر چی پیدا شد خاموش
        if (winPanel != null) winPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        if (winPanel == null) Debug.LogError("WinPanel not found (even inactive). Set Tag=WinPanel on your win panel.");
        if (gameOverPanel == null) Debug.LogError("LosePanel not found (even inactive). Set Tag=LosePanel on your lose panel.");
    }

    private GameObject FindInSceneByTagIncludingInactive(string tag)
    {
        var all = Resources.FindObjectsOfTypeAll<GameObject>();
        var activeScene = SceneManager.GetActiveScene();

        foreach (var go in all)
        {
            if (!go) continue;
            if (go.hideFlags != HideFlags.None) continue; // چیزهای ادیتوری رو رد کن
            if (go.scene != activeScene) continue;        // فقط همین صحنه
            if (go.CompareTag(tag)) return go;
        }
        return null;
    }

    private GameObject FindInSceneByNameIncludingInactive(string name)
    {
        var all = Resources.FindObjectsOfTypeAll<GameObject>();
        var activeScene = SceneManager.GetActiveScene();

        foreach (var go in all)
        {
            if (!go) continue;
            if (go.hideFlags != HideFlags.None) continue;
            if (go.scene != activeScene) continue;
            if (go.name == name) return go;
        }
        return null;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        else Debug.LogError("LosePanel is null in this scene!");
    }

    public void Win()
    {
        Time.timeScale = 0f;
        if (winPanel != null) winPanel.SetActive(true);
        else Debug.LogError("WinPanel is null in this scene!");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(FIRST_LEVEL_INDEX);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;

        int idx = SceneManager.GetActiveScene().buildIndex;
        if (idx == MENU_INDEX)
        {
            SceneManager.LoadScene(FIRST_LEVEL_INDEX);
            return;
        }

        int next = idx + 1;
        if (next < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(next);
        else
            SceneManager.LoadScene(MENU_INDEX); // آخرین لول -> منو
    }

    public void QuitGame()
    {
        Application.Quit();
    }
   }
