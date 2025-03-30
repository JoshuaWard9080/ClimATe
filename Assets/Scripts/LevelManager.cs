using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextLevelSceneName;
    [SerializeField] private TextMeshProUGUI timeText;
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
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                Debug.Log("Scene loaded: " + scene.name);

                // Rebind ONLY if it's a gameplay level scene
                if (scene.name == "Level_1" ||
                    scene.name == "Level_2" ||
                    scene.name == "Level_3" ||
                    scene.name == "Level_4")
                {
                    StartCoroutine(WaitUntilSceneObjectsAreReady());
                }
            };

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // if (timeText == null)
        // {
        //     timeText = GameObject.Find("TimeText")?.GetComponent<TextMeshProUGUI>();
        //     Debug.LogWarning("timeText auto-bound: " + (timeText != null));
        // }

        LevelStatsManager.Instance?.StartLevelTimer();
    }

    void Update()
    {
        //Debug.Log($"[Update] isPaused: {isPaused}, elapsed: {LevelStatsManager.Instance?.elapsedTime}");

        LevelStatsManager.Instance.UpdateTimer();
        float time = LevelStatsManager.Instance.elapsedTime;

        if (!isPaused && LevelStatsManager.Instance != null)
        {
            //timeText.text = "TIMER RUNNING! YUH";
            timeText.text = string.Format("{0:00}:{1:00}", Mathf.Floor(time / 60), time % 60);
        }
        else
        {
            Debug.LogWarning("timeText is null in LevelManager! bro :(");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape pressed, loading escape menu...");
            ToggleEscapeMenu();
        }
    }

    private IEnumerator WaitUntilSceneObjectsAreReady()
    {
        Debug.Log("Waiting for TimeText & EscapeCanvas...");

        // Wait for scene to be fully active and objects to spawn
        while (SceneManager.GetActiveScene().name.StartsWith("MainMenu"))
            yield return null;

        // Wait for key UI objects to appear
        GameObject canvas = null;
        GameObject time = null;

        for (int i = 0; i < 40; i++)
        {
            canvas = GameObject.Find("EscapeCanvas");
            time = GameObject.Find("TimeText");

            if (canvas != null && time != null)
                break;

            yield return new WaitForSeconds(0.025f);
        }

        if (canvas != null)
        {
            escapeMenuPanel = canvas.transform.Find("EscapeMenuPanel")?.gameObject;
            quitConfirmationPopup = canvas.transform.Find("QuitConfirmationPopup")?.gameObject;
            menuNavigator = escapeMenuPanel?.GetComponent<MenuNavigator>();
            escapeMenuPanel?.SetActive(false);

            Debug.Log("EscapeCanvas bound.");
        }
        else
        {
            Debug.LogWarning("EscapeCanvas not found.");
        }

        if (time != null)
        {
            timeText = time.GetComponent<TextMeshProUGUI>();
            Debug.Log("timeText bound: " + timeText.name);
        }
        else
        {
            Debug.LogWarning("Could not bind TimeText.");
        }
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     StartCoroutine(RebindUIElements());
    // }

    private IEnumerator RebindUIElements()
    {
        TextMeshProUGUI foundText = null;
        for (int i = 0; i < 30; i++)
        {
            var find = GameObject.Find("TimeText");
            if (find != null)
            {
                foundText = find.GetComponent<TextMeshProUGUI>();
                if (foundText != null)
                {
                    break;
                }
            }

            yield return new WaitForSeconds(0.03f);
        }

        if (foundText != null)
        {
            timeText = foundText;
            Debug.Log("timeText rebound successfully");
        }
        else
        {
            Debug.LogWarning("Could not find TimeText in the scene after waiting, not sigma");
        }

        // yield return null;

        // for (int i = 0; i < 3; i++) yield return null;

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

    // void Start()
    // {
    //     if (escapeMenuPanel != null)
    //     {
    //         escapeMenuPanel.SetActive(false);
    //     }
    // }

    public void ToggleEscapeMenu()
    {
        isPaused = !isPaused;

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
        LevelStatsManager.Instance?.UpdateGlobalStats();

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
