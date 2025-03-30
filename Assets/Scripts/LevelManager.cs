using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextLevelSceneName;
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
        LevelStatsManager.Instance?.StartLevelTimer();
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(RebindUIElements());
    }

    private IEnumerator RebindUIElements()
    {
        yield return null;

        for (int i = 0; i < 3; i++) yield return null;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape pressed, loading escape menu...");
            ToggleEscapeMenu();
        }
    }

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