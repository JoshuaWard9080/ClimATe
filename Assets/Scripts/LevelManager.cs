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
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private float transitionTime = 2f;

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

    public void LevelCompleteTransition()
    {
        Time.timeScale = 0f;
        StartCoroutine(PlayLevelCompleteSequence());
    }

    private IEnumerator PlayLevelCompleteSequence()
    {
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("SlideUp");
        }

        yield return new WaitForSecondsRealtime(transitionTime);

        Time.timeScale = 1f;
        CompleteLevel();
    }
}
