using Unity.VisualScripting;
using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
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
        if (currentLives > 1)
        {
            currentLives--;
            LevelStatsManager.Instance.totalLivesLost++;

            if (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }
        else
        {
            PlayerDied();
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
    }
}
