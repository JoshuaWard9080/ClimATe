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

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
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
