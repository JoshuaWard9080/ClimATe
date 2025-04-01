using UnityEngine;

public class LevelTracker : MonoBehaviour
{
    public static LevelTracker Instance;

    [HideInInspector] public string currentLevelScene;
    [HideInInspector] public string nextLevelScene;

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
}
