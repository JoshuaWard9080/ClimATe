using UnityEngine;

public class GlobalStatsManager : MonoBehaviour
{
    public static GlobalStatsManager Instance;

    public int totalLivesLost = 0;
    public float totalTime = 0f;
    public int totalFuzzyKills = 0;
    public int totalYetiKills = 0;
    public int totalBirdKills = 0;
    public int totalKills = 0;
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

    public void AddStats(int lives, float time, int fuzzyKills, int yetKills, int birdKills, int kills, int points)
    {
        totalLivesLost += lives;
        totalTime += time;
        totalFuzzyKills += fuzzyKills;
        totalYetiKills += yetKills;
        totalBirdKills += birdKills;
        totalKills += kills;
        totalPoints += points;
    }

    public void ResetStats()
    {
        totalLivesLost = 0;
        totalTime = 0f;
        totalFuzzyKills = 0;
        totalYetiKills = 0;
        totalBirdKills = 0;
        totalKills = 0;
        totalPoints = 0;
    }
}
