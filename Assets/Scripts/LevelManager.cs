using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextLevelSceneName;
    [SerializeField] private GameObject levelFailedPanel;

    void Start()
    {
        if (levelFailedPanel != null)
        {
            levelFailedPanel.SetActive(false);
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
        if (levelFailedPanel != null)
        {
            levelFailedPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadEscapeMenu()
    {
        //load escape menu

    }
}
