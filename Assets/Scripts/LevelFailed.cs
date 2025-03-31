using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelFailed : MonoBehaviour
{
    public static LevelFailed Instance;

    [Header("UI Elements")]
    [SerializeField] private GameObject diedPanel;
    [SerializeField] private Button retryLevelButton;
    [SerializeField] private Button mainMenuButton;

    [Header("Audio")]
    [SerializeField] AudioSource switchButtonAudio;
    [SerializeField] AudioSource clickButtonAudio;

    [Header("Text Animation")]
    [SerializeField] private TextAnimation textAnimator;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        // Rebind if fields were lost due to scene load
        if (diedPanel == null)
        {
            /*
                Heirarchy path search, to look for the PlayerDiedPanel inside the UI folder, otherwise it cant find it
            */
            Transform panelT = transform.Find("PlayerDiedPanel");
            diedPanel = panelT?.gameObject;
            Debug.Log(diedPanel != null ? "Rebound diedPanel" : "Failed to find diedPanel");
        }

        if (retryLevelButton == null)
        {
            retryLevelButton = transform.Find("PlayerDiedPanel/RestartButton")?.GetComponent<Button>();
        }

        if (mainMenuButton == null)
        {
            mainMenuButton = transform.Find("PlayerDiedPanel/MainMenuButton")?.GetComponent<Button>();
        }
    }

    void Start()
    {
        diedPanel.SetActive(false);

        retryLevelButton.onClick.AddListener(GoToLevel1);
        mainMenuButton.onClick.AddListener(MainMenu);   
    }

    public void ShowGameOver()
    {
        if (diedPanel == null)
        {
            Debug.LogWarning("diedPanel was null at ShowGameOver, rebinding...");
            diedPanel = transform.Find("PlayerDiedPanel")?.gameObject;
        }

        if (diedPanel == null)
        {
            Debug.LogError("STILL couldn't find diedPanel bruh :( GameOver UI can't display.");
            return;
        }

        Debug.Log("Setting diedPanel to active.");
        diedPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoToLevel1()
    {
        if (clickButtonAudio != null)
        {
            clickButtonAudio.Play();
        }

        Debug.Log("Heading to Level 1...");

        LevelStatsManager.Instance?.ResetLevelTimer();
        LevelStatsManager.Instance?.ResetAllStats();

        Time.timeScale = 1f;

        LevelStatsManager.Instance?.StartLevelTimer();
        SceneManager.LoadScene("Level_1");
    }

    public void MainMenu()
    {
        if (clickButtonAudio != null)
        {
            clickButtonAudio.Play();
        }

        LevelStatsManager.Instance?.ResetLevelTimer();
        LevelStatsManager.Instance?.ResetAllStats();

        Time.timeScale = 1f;

        Debug.Log("Loading main menu...");
        SceneManager.LoadScene("MainMenu");
    }
}