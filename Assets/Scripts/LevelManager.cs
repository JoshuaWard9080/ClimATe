using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextLevelSceneName;
    [SerializeField] private GameObject escapeMenuPanel;
    [SerializeField] private GameObject quitConfirmationPopup;

    public static LevelManager Instance;
    private bool isPaused = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (escapeMenuPanel != null)
        {
            escapeMenuPanel.SetActive(false);
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

    public void ToggleEscapeMenu()
    {
        isPaused = !isPaused;

        if (escapeMenuPanel != null)
        {
            escapeMenuPanel.SetActive(isPaused);
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
            escapeMenuPanel.SetActive(false);
        }
    }

    public void ConfirmQuitToMainMenu()
    {
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
        string current = SceneManager.GetActiveScene().name;
        string next = "";

        // Manually define next level logic
        if (current == "Level_1") next = "Level_2";
        else if (current == "Level_2") next = "Level_3";
        else if (current == "Level_3") next = "Level_4";
        else next = "MainMenu";

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
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
