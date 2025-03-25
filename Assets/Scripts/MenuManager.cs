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
    [SerializeField] AudioSource menuMusic;

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
    }

    private void SelectButton(int index)
    {
        EventSystem.current.SetSelectedGameObject(buttons[index].gameObject);
    }

    public void StartGame()
    {
        //stop the menu music from playing before switching to main menu
        if (menuMusic.isPlaying)
        {
            menuMusic.Stop();
        }

        Debug.Log("Starting game...");
        SceneManager.LoadScene("Level_1");
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
}
