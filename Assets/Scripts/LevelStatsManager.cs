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
    public int fishCollected = 0;
    public int blocksDestroyed = 0;
    public int iciclesDestroyed = 0;

    private float startTime;
    public float elapsedTime;
    public float pausedTime;

    private bool isTiming = false;

    //lives counters
    public int maxLives = 8;
    public int remainingLives = 8;
    public int livesAtLevelStart = 8;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("LevelStatsManager initialized");
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
            //Debug.Log("Updating Timer: " + (Time.time - startTime));
            UpdateTimer();
        }
    }

    public void StartLevelTimer()
    {
        startTime = Time.time;
        elapsedTime = 0f;
        isTiming = true;
        Debug.Log("Timer started at " + startTime);
    }

    public void EndLevelTimer()
    {
        totalTime = Time.time - startTime;
        Debug.Log("Level TIme: " + totalTime);
    }

    public void PauseLevelTimer()
    {
        if (!isTiming)
        {
            return;
        }

        pausedTime = Time.time;
        isTiming = false;
    }

    public void ResumeLevelTimer()
    {
        if (isTiming)
        {
            return;
        }

        float pausedDuration = Time.time - pausedTime;
        startTime += pausedDuration;
        isTiming = true;
    }

    public void ResetLevelTimer()
    {
        elapsedTime = 0f;
        pausedTime = 0f;
        startTime = 0f;
        isTiming = false;
    }

    public void UpdateTimer()
    {
        elapsedTime = Time.time - startTime;
    }

    public void AddStats(int lives, float time, int yetiKills, int birdKills, int kills, int blocks, int icicles, int fruits, int points)
    {
        totalLivesLost += lives;
        totalTime += time;
        totalYetiKills += yetiKills;
        totalBirdKills += birdKills;
        totalKills += totalYetiKills + totalBirdKills;
        blocksDestroyed += blocks;
        iciclesDestroyed += icicles;
        fishCollected += fruits;
        totalPoints += points;
    }

    public void ResetAllStats()
    {
        totalLivesLost = 0;
        totalTime = 0f;
        totalYetiKills = 0;
        totalBirdKills = 0;
        totalKills = 0;
        blocksDestroyed = 0;
        iciclesDestroyed = 0;
        fishCollected = 0;
        totalPoints = 0;
        remainingLives = maxLives;
    }

    public void ResetLevelStats()
    {
        totalYetiKills = 0;
        totalBirdKills = 0;
        totalKills = 0;
        blocksDestroyed = 0;
        iciclesDestroyed = 0;
        fishCollected = 0;
        totalPoints = 0;
        ResetLevelTimer();
    }

    public void UpdateGlobalStats()
    {
        GlobalStatsManager.Instance.AddStats(totalLivesLost, totalTime, totalYetiKills, totalBirdKills, totalKills, blocksDestroyed, iciclesDestroyed, fishCollected, totalPoints);
    }

    public int CalculateLevelPoints()
    {
        int lifePoints = remainingLives * 10;
        int killPoints = (totalYetiKills * 10) + (totalBirdKills * 15);
        float maxBonusTime = 120f;
        int timePoints = Mathf.RoundToInt(Mathf.Clamp(maxBonusTime - elapsedTime, 0f, maxBonusTime) * 2);
        int fishPoints = fishCollected * 5;
        int blockPoints = blocksDestroyed;
        int iciclePoints = iciclesDestroyed * 5;

        int levelPoints = lifePoints + killPoints + timePoints + fishPoints + blockPoints + iciclePoints;
        totalPoints += levelPoints;

        Debug.Log("Level Points: " + levelPoints);

        return levelPoints;
    }

    //debugging

    // public int RemainingLives
    // {
    //     get => remainingLives;
    //     set
    //     {
    //         Debug.LogWarning($"[LIVES DEBUG] Setting remainingLives to {value}.\n" + new System.Diagnostics.StackTrace(true));
    //         remainingLives = value;
    //     }
    // }
}