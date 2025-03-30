using System.Timers;
using UnityEngine;

public class LevelStatsManager : MonoBehaviour
{
    public static LevelStatsManager Instance;
    public int totalLivesLost = 0;
    public float totalTime = 0f;
    public int totalYetiKills = 0;
    public int totalBirdKills = 0;
    public int totalKills = 0;
    public int totalPoints = 0;

    private float startTime;
    public float elapsedTime;

    private bool isTiming = false;

    private void Awake()
    {
        if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("[LSM] LevelStatsManager initialized");
    }
    else
    {
        Destroy(gameObject);
    }
    }

    private void Update()
    {
        if (isTiming)
        {
            Debug.Log("[LSM] Updating Timer: " + (Time.time - startTime));
            UpdateTimer();
        }
    }

    public void StartLevelTimer()
    {
        startTime = Time.time;
        elapsedTime = 0f;
        isTiming = true;
            Debug.Log("[LSM] Timer started at " + startTime);

    }

    public void EndLevelTimer()
    {
        totalTime = Time.time - startTime;
        Debug.Log("Level TIme: " + totalTime);
    }

    public void UpdateTimer()
    {
        elapsedTime = Time.time - startTime;
    }

    public void AddStats(int lives, float time, int yetKills, int birdKills, int kills, int points)
    {
        totalLivesLost += lives;
        totalTime += time;
        totalYetiKills += yetKills;
        totalBirdKills += birdKills;
        totalKills += kills;
        totalPoints += points;
    }

    public void ResetStats()
    {
        totalLivesLost = 0;
        totalTime = 0f;
        totalYetiKills = 0;
        totalBirdKills = 0;
        totalKills = 0;
        totalPoints = 0;
    }

    public void UpdateGlobalStats()
    {
        GlobalStatsManager.Instance.AddStats(totalLivesLost, totalTime, totalYetiKills, totalBirdKills, totalKills, totalPoints);
    }
}
