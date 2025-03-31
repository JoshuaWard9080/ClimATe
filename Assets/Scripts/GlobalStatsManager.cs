using UnityEngine;

public class GlobalStatsManager : MonoBehaviour
{
    public static GlobalStatsManager Instance;

    public int totalLivesLost = 0;
    public float totalTime = 0f;
    public int totalYetiKills = 0;
    public int totalBirdKills = 0;
    public int totalKills = 0;
    public int blocksDestroyed = 0;
    public int iciclesDestroyed = 0;
    public int fishCollected = 0;
    public int totalPoints = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
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

    public void AddStats(int lives, float time, int yetKills, int birdKills, int kills, int blocks, int icicles, int fish, int points)
    {
        totalLivesLost += lives;
        totalTime += time;
        totalYetiKills += yetKills;
        totalBirdKills += birdKills;
        totalKills += kills;
        blocksDestroyed += blocks;
        iciclesDestroyed += icicles;
        fishCollected += fish;
        totalPoints += points;
    }

    public void ResetStats()
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
    }
}
