using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextLevelSceneName;
    private GameObject escapeMenuPanel;
    private GameObject quitConfirmationPopup;
    private MenuNavigator menuNavigator;

    public static LevelManager Instance;
    private bool isPaused = false;
    private bool uiReady = false;


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
        Debug.Log("LevelManager start RAHHHHHHH");
        LevelStatsManager.Instance?.ResetLevelTimer();
        LevelStatsManager.Instance?.StartLevelTimer();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!uiReady)
            {
                Debug.LogWarning("UI not ready yet — ignoring Escape key.");
                return;
            }

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
    }

    private IEnumerator DelaySetLivesAtLevelStart()
    {
        yield return null;
        LevelStatsManager.Instance.livesAtLevelStart = LevelStatsManager.Instance.remainingLives;
    }

    private IEnumerator RebindUIElements()
    {
        yield return null;

        for (int i = 0; i < 3; i++) yield return null;
Debug.Log("Rebinding EscapeCanvas...");

        var canvas = GameObject.FindObjectsOfType<Canvas>(true)
            .FirstOrDefault(c => c.name == "EscapeCanvas")?.gameObject;

        Debug.Log("Canvas found: " + canvas);
        Debug.Log("EscapePanel: " + escapeMenuPanel);
        Debug.Log("MenuNav: " + menuNavigator);


        if (canvas == null)
        {
            Debug.Log("EscapeCanvas not found.");
            yield break;
        }
        else
        {
            Debug.Log("EscapeCanvas found yay.");
        }

        escapeMenuPanel = canvas.GetComponentsInChildren<Transform>(true)
            .FirstOrDefault(t => t.name == "EscapeMenuPanel")?.gameObject;

        quitConfirmationPopup = canvas.GetComponentsInChildren<Transform>(true)
            .FirstOrDefault(t => t.name == "QuitConfirmationPopup")?.gameObject;

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

        Debug.Log("EscapeMenuPanel and MenuNavigator linked.");
        uiReady = true;
    }

    public void ToggleEscapeMenu()
    {
        if (!uiReady || escapeMenuPanel == null)
        {
            Debug.LogWarning("Escape menu UI not ready");
            return;
        }

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

        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        LevelStatsManager.Instance.ResumeLevelTimer();
        escapeMenuPanel.SetActive(false);
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
        LevelStatsManager.Instance?.EndLevelTimer();

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
        isPaused = false;
        Time.timeScale = 1f;
        LevelStatsManager.Instance.ResetLevelTimer();
        LevelStatsManager.Instance.StartLevelTimer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
