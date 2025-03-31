using Unity.VisualScripting;
using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    public GameObject heart;
    public int maxLives = 8;
    private int currentLives;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLives = maxLives;
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

        if (currentLives > 0)
        {
            currentLives--;
            LevelStatsManager.Instance.totalLivesLost++;

            if (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }

            if (currentLives == 0)
            {
                PlayerDied();
            }
        }
    }

    public void ResetLives()
    {
        currentLives = maxLives;
        LevelStatsManager.Instance.totalLivesLost = 0;
        ResetHearts();
    }

    private void ResetHearts()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentLives; i++)
        {
            Instantiate(heart, transform);
        }
    }

    public void PlayerDied()
    {
        //call player died screen, so losing screen, then send them back to Main Menu
        Debug.Log("Player has run out of lives");

        if (LevelFailed.Instance != null)
        {
            LevelFailed.Instance.ShowGameOver();
        }
        else
        {
            Debug.Log("LevelFailed instance is null");
        }
    }
}
