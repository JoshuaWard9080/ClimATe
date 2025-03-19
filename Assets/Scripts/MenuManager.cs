using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button onePlayerStartButton;
    [SerializeField] private Button quitButton;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            selectedIndex = (selectedIndex - 1 + buttons.Length) % buttons.Length;
            SelectButton(selectedIndex);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            selectedIndex = (selectedIndex + 1 + buttons.Length) % buttons.Length;
            SelectButton(selectedIndex);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            buttons[selectedIndex].onClick.Invoke();
        }
    }

    private void SelectButton(int index) {
        EventSystem.current.SetSelectedGameObject(buttons[index].gameObject);
    }

    private void StartGame()
    {
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
}
