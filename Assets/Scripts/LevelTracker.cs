using UnityEditor.PackageManager;
using UnityEngine;

public class LevelTracker : MonoBehaviour
{
    public static LevelTracker Instance;

    public string currentLevelScene;
    public string nextLevelScene;

    void Awake()
    {
        //making sure LevelTracker is persisted across scenes, so it can track the level correctly
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCurrentLevel(string sceneName)
    {
        currentLevelScene = sceneName;

        if (sceneName == "Level_1")
        {
            nextLevelScene = "Level_2";
        }
        else if (sceneName == "Level_2")
        {
            nextLevelScene = "Level_3";
        }
        else
        {
            nextLevelScene = "Level_4";
        }
    }
}
