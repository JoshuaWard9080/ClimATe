using UnityEngine;
using UnityEngine.SceneManagement;

public class SetCurrAndNextLevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (LevelTracker.Instance != null)
        {
            string current = SceneManager.GetActiveScene().name;
            LevelTracker.Instance.SetCurrentLevel(current);
        }
    }
}
