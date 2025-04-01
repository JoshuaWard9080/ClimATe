using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextLevelSceneName;

    [Header("Audio")]
    [SerializeField] private AudioSource levelMusic;

    private GameObject escapeMenuPanel;
    private GameObject quitConfirmationPopup;
    private MenuNavigator menuNavigator;

    public static LevelManager Instance;
    private bool isPaused = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("LevelManager started, starting timer and music");

        if (LevelStatsManager.Instance != null)
        {
            LevelStatsManager.Instance.ResetLevelTimer();
            LevelStatsManager.Instance.StartLevelTimer();
        }
        else
        {
            Debug.LogWarning("LevelStatsManager.Instance is null in LevelManager.Start()");
        }

        if (levelMusic != null)
        {
            levelMusic.Play();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape pressed, loading escape menu...");
            ToggleEscapeMenu();
        }
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(RebindUIElements());
        StartCoroutine(DelaySetLivesAtLevelStart());

        if (scene.name.StartsWith("Level_"))
        {
            Debug.Log("Starting level timer for scene: " + scene.name);

            if (LevelStatsManager.Instance != null)
            {
                LevelStatsManager.Instance.ResetLevelTimer();
                LevelStatsManager.Instance.StartLevelTimer();
            }
            else
            {
                Debug.LogWarning("No LevelStatsManager found in " + scene.name);
            }
        }
    }

    private IEnumerator DelaySetLivesAtLevelStart()
    {
        yield return null;
        LevelStatsManager.Instance.livesAtLevelStart = LevelStatsManager.Instance.remainingLives;
    }

    private IEnumerator RebindUIElements()
    {
        yield return null;

        for (int i = 0; i < 3; i++)
        {
            yield return null;
        }

        var canvas = GameObject.Find("EscapeCanvas");
        if (canvas == null)
        {
            Debug.Log("EscapeCanvas not found.");
            yield break;
        }

        escapeMenuPanel = canvas.transform.Find("EscapeMenuPanel")?.gameObject;
        quitConfirmationPopup = canvas.transform.Find("QuitConfirmationPopup")?.gameObject;

        if (escapeMenuPanel != null)
        {
            escapeMenuPanel.SetActive(false);
            menuNavigator = escapeMenuPanel.GetComponent<MenuNavigator>();
            Debug.Log("EscapeMenuPanel and MenuNavigator linked.");
        }
        else
        {
            Debug.Log("EscapeMenuPanel not found or missing MenuNavigator.");
        }
    }

    public void ToggleEscapeMenu()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            LevelStatsManager.Instance.PauseLevelTimer();
        }
        else
        {
            LevelStatsManager.Instance.ResumeLevelTimer();
        }

        if (escapeMenuPanel != null)
        {
            escapeMenuPanel.SetActive(isPaused);

            if (isPaused)
            {
                menuNavigator?.ResetSelection();
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        if (levelMusic != null)
        {
            levelMusic.Pause();
        }

        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        LevelStatsManager.Instance.ResumeLevelTimer();
        escapeMenuPanel.SetActive(false);

        if (levelMusic != null)
        {
            levelMusic.UnPause();
        }

        Time.timeScale = 1f;
    }

    public void LoadMainMenu()
    {
        if (quitConfirmationPopup != null)
        {
            quitConfirmationPopup.SetActive(true);
            escapeMenuPanel?.SetActive(false);

            var quitNav = quitConfirmationPopup.GetComponent<MenuNavigator>();
            quitNav?.ResetSelection();
        }
    }

    public void ConfirmQuitToMainMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        LevelStatsManager.Instance?.PauseLevelTimer();
        LevelStatsManager.Instance?.ResetLevelTimer();
        SceneManager.LoadScene("MainMenu");
    }

    public void CancelQuitToMainMenu()
    {
        if (quitConfirmationPopup != null)
        {
            quitConfirmationPopup.SetActive(false);
        }

        if (escapeMenuPanel != null)
        {
            escapeMenuPanel.SetActive(true);
        }
    }

    public void CompleteLevel()
    {
        if (levelMusic != null)
        {
            levelMusic.Stop();
        }

        string current = SceneManager.GetActiveScene().name;
        string next = "";

        // Manually define next level logic
        if (current == "Level_1")
        {
            next = "Level_2";
        }
        else if (current == "Level_2")
        {
            next = "Level_3";
        }
        else if (current == "Level_3")
        {
            next = "Level_4";
        }
        else
        {
            next = "VictoryScene";
        }

        LevelTracker.Instance.currentLevelScene = current;
        LevelTracker.Instance.nextLevelScene = next;

        Debug.Log("Set current to: " + current);
        Debug.Log("Set next to: " + next);

        SceneManager.LoadScene("LevelComplete");
    }

    public void FailLevel()
    {
        if (escapeMenuPanel != null)
        {
            escapeMenuPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void RetryLevel()
    {
        if (levelMusic != null)
        {
            //replay from beginning
            levelMusic.Stop();
            levelMusic.Play();
        }

        isPaused = false;
        Time.timeScale = 1f;
        
        if (LevelStatsManager.Instance != null)
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == "Level_1")
            {
                LevelStatsManager.Instance.remainingLives = LevelStatsManager.Instance.maxLives;
            }
            else
            {
                LevelStatsManager.Instance.remainingLives = LevelStatsManager.Instance.livesAtLevelStart;
            }

            LevelStatsManager.Instance.ResetLevelTimer();
            LevelStatsManager.Instance.StartLevelTimer();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
