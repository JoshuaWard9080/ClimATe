using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LevelCompleteManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private TMP_Text totalPointsText;
    [SerializeField] AudioSource switchButtonAudio;
    [SerializeField] AudioSource clickButtonAudio;
    [SerializeField] AudioSource levelCompleteAudio;
    public int totalPoints = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //play the audio, loops continuously while the menu is open
        if (levelCompleteAudio != null && !levelCompleteAudio.isPlaying)
        {
            levelCompleteAudio.loop = true;
            levelCompleteAudio.Play();
            Debug.Log("Level complete music started and set to loop.");
        }

        //get the player stats from the previous level and update them here
        //probably something like setText("Kill Count: " + playerStats.getKillCount())

        bool isFinalLevel = LevelTracker.Instance != null && LevelTracker.Instance.nextLevelScene == "VictoryScene";

        if (isFinalLevel)
        {
            Debug.Log("Final level complete, loading VictoryScene");

            //nextLevelButton.gameObject.SetActive(false);
            mainMenuButton.gameObject.SetActive(false);
            quitGameButton.gameObject.SetActive(false);

            nextLevelButton.onClick.AddListener(() =>
            {
                if (levelCompleteAudio.isPlaying)
                {
                    levelCompleteAudio.Stop();
                }

                SceneManager.LoadScene("VictoryScene");
            });
        }
        else
        {
            if (nextLevelButton != null)
            {
                //not sure what the next level wil be yet
                nextLevelButton.onClick.AddListener(NextLevel);
            }

            if (mainMenuButton != null)
            {
                mainMenuButton.onClick.AddListener(MainMenu);
            }

            if (quitGameButton != null)
            {
                quitGameButton.onClick.AddListener(QuitGame);
            }
        }

        UpdatePointsUI();
    }

    public void NextLevel()
    {
        //Probably make an if statement where if the current level is 1 then load 2, if the current level is 2 load 3, etc.

        Debug.Log("=== NEXT LEVEL PRESSED ===");
        Debug.Log("LevelTracker.Instance is null? " + (LevelTracker.Instance == null));
        Debug.Log("Current Level: " + LevelTracker.Instance.currentLevelScene);
        Debug.Log("Next Level: " + LevelTracker.Instance.nextLevelScene);


        Debug.Log("Loading next level...");

        //uses the LevelTracker to figure out which level is next
        SceneManager.LoadScene(LevelTracker.Instance.nextLevelScene);
        Debug.Log("Loading next level: " + LevelTracker.Instance.currentLevelScene);
    }

    public void MainMenu()
    {
        //stop the menu music from playing before switching to main menu
        if (levelCompleteAudio.isPlaying)
        {
            levelCompleteAudio.Stop();
        }

        Debug.Log("Loading main menu...");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void AddPoints(int amount)
    {
        totalPoints += amount;
        Debug.Log("Points Updated: " + totalPoints);

        UpdatePointsUI();
    }

    private void UpdatePointsUI()
    {
        totalPointsText.text = "Total Points: " + totalPoints;
    }
}
