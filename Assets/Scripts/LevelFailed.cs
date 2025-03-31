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
        Debug.Log("From LevelFailed, Awake() ran!");
        Instance = this;
    }

    void Start()
    {
        diedPanel.SetActive(false);

        retryLevelButton.onClick.AddListener(GoToLevel1);
        mainMenuButton.onClick.AddListener(MainMenu);   
    }

    public void ShowGameOver()
    {
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
        LevelStatsManager.Instance?.ResetStats();

        Time.timeScale = 1f;

        SceneManager.LoadScene("Level_1");
    }

    public void MainMenu()
    {
        if (clickButtonAudio != null)
        {
            clickButtonAudio.Play();
        }

        LevelStatsManager.Instance?.ResetLevelTimer();
        LevelStatsManager.Instance?.ResetStats();

        Time.timeScale = 1f;

        Debug.Log("Loading main menu...");
        SceneManager.LoadScene("MainMenu");
    }
}
