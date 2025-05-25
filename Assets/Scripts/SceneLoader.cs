using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //loads the specific scene
    public void LoadScene(string sceneName)
    {
        Debug.Log($"Loading scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    //once scene is loaded it fades into the scene
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var fade = FindFirstObjectByType<CloudFadeUI>();
        if (fade != null)
        {
            fade.FadeOut(null);
        }
    }
}
