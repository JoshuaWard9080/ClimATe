using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

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

    //array to know which button the user is hovering over with the keyboard
    private Button[] buttons;
    private int selectedIndex = 0;

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
        //probably something like setText("Kill Count: " + playerStats.getKillCount())??? 


        buttons = new Button[] { nextLevelButton, mainMenuButton, quitGameButton };

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

        SelectButton(selectedIndex);

        UpdatePointsUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            selectedIndex = (selectedIndex - 1 + buttons.Length) % buttons.Length;
            SelectButton(selectedIndex);
            switchButtonAudio.Play();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            selectedIndex = (selectedIndex + 1 + buttons.Length) % buttons.Length;
            SelectButton(selectedIndex);
            switchButtonAudio.Play();
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            buttons[selectedIndex].onClick.Invoke();
            clickButtonAudio.Play();
        }
    }

    private void SelectButton(int index)
    {
        EventSystem.current.SetSelectedGameObject(buttons[index].gameObject);
    }

    private void NextLevel()
    {
        //Probably make an if statement where if the current level is 1 then load 2, if the current level is 2 load 3, etc.

        Debug.Log("Loading next level...");
        SceneManager.LoadScene("NextLevel"); //logic for next level needs to be implemented
    }

    private void MainMenu()
    {
        //stop the menu music from playing before switching to main menu
        if (levelCompleteAudio.isPlaying)
        {
            levelCompleteAudio.Stop();
        }

        Debug.Log("Loading main menu...");
        SceneManager.LoadScene("MainMenu");
    }

    private void QuitGame()
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
