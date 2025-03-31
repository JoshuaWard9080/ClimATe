using Unity.VisualScripting;
using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    public GameObject heart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetHearts();
    }

    //TODO: comment this Update method out when not testing, this is purely to ensure the TakeDamage method is working as expected
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        if (LevelStatsManager.Instance == null)
        {
            Debug.LogError("LevelStatsManager.Instance is NULL!!");
        }

        if (LevelStatsManager.Instance.remainingLives > 0)
        {
            LevelStatsManager.Instance.remainingLives--;
            LevelStatsManager.Instance.totalLivesLost++;

            if (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }

            if (LevelStatsManager.Instance.remainingLives == 0)
            {
                PlayerDied();
            }
        }
    }

    public void ResetLives()
    {
        LevelStatsManager.Instance.remainingLives = LevelStatsManager.Instance.maxLives;
        LevelStatsManager.Instance.totalLivesLost = 0;
        ResetHearts();
    }

    private void ResetHearts()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < LevelStatsManager.Instance.remainingLives; i++)
        {
            Instantiate(heart, transform);
        }
    }

    public void PlayerDied()
    {
        //call player died screen, so losing screen, then send them back to Main Menu
        if (LevelFailed.Instance != null)
        {
            LevelFailed.Instance.ShowGameOver();
        }
        else
        {
            Debug.LogError("LevelFailed instance is null");
        }
    }
}