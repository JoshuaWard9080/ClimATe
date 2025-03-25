using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextLevelSceneName;
    [SerializeField] private GameObject escapeMenuPanel;
    [SerializeField] private GameObject quitConfirmationPopup;
    private bool isPaused = false;

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
        }
    }

    public void ConfirmQuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void CancleQuitToMainMenu()
    {
        if (quitConfirmationPopup != null)
        {
            quitConfirmationPopup.SetActive(false);
        }
    }

    public void CompleteLevel()
    {
        LevelTracker.Instance.currentLevelScene = SceneManager.GetActiveScene().name;
        LevelTracker.Instance.nextLevelScene = nextLevelSceneName;

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
