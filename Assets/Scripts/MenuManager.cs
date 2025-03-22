using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button onePlayerStartButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TMP_Text totalPointsText;
    [SerializeField] AudioSource menuMusic;
    [SerializeField] AudioSource switchButtonAudio;
    [SerializeField] AudioSource clickButtonAudio;
    public int totalPoints = 0;

    //arrary to know which button the user is hovering over with the keyboard
    private Button[] buttons;
    private int selectedIndex = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    private void Start()
    {
        //play the audio, loops continuously while the menu is open
        if (menuMusic != null && !menuMusic.isPlaying)
        {
            menuMusic.loop = true;
            menuMusic.Play();
            Debug.Log("Main menu music started and set to loop.");
        }

        buttons = new Button[] { onePlayerStartButton, quitButton };

        if (onePlayerStartButton != null)
        {
            onePlayerStartButton.onClick.AddListener(StartGame);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }

        SelectButton(selectedIndex);

        UpdatePointsUI();
    }

    private void Update()
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

    private void StartGame()
    {
        //stop the menu music from playing before switching to main menu
        if (menuMusic.isPlaying)
        {
            menuMusic.Stop();
        }

        Debug.Log("Starting game...");
        SceneManager.LoadScene("Level1");
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
