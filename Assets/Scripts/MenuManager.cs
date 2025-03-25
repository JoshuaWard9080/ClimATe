using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button onePlayerStartButton;
    [SerializeField] private Button quitButton;
    [SerializeField] AudioSource menuMusic;

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

        if (onePlayerStartButton != null)
        {
            onePlayerStartButton.onClick.AddListener(StartGame);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
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
