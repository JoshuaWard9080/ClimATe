using System;
using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    public GameObject heart;
    public int invincibilityFrames = 0;

    [Header("Audio")]
    [SerializeField] private AudioSource takeDamageAudio;
    [SerializeField] private AudioSource playerDiedAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetHearts();
    }

     void Update()
     {
         if (invincibilityFrames > 0) invincibilityFrames--;
     }

    public void TakeDamage()
    {
        Debug.Log("number of invincibility frames: " + invincibilityFrames);
        if (invincibilityFrames > 0) return;

        if (takeDamageAudio != null)
        {
            takeDamageAudio.Play();
        }

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
        invincibilityFrames = 400;
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
        if (playerDiedAudio != null)
        {
            playerDiedAudio.Play();
        }

        ClearHearts(); //remove all hearts from screen (if they exist) --> Specifically for when the piling snow kills the player

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

    public void ClearHearts()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}