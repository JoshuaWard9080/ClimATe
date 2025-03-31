using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Timers;
using UnityEngine.SceneManagement;

public class VictorySceneManager : MonoBehaviour
{
    public RectTransform statsContent;
    public float scrollSpeed = 20f;
    public float endDelay = 3f;
    public GameObject returnToMenuText;

    private bool scrollStarted = false;
    private bool scrollEnded = false;

    private Vector2 startPosition;
    private Vector2 endPosition;

    [Header("Text Fields")]
    public TextMeshProUGUI livesLostText;
    public TextMeshProUGUI timePlayedText;
    public TextMeshProUGUI yetiKillsText;
    public TextMeshProUGUI birdKillsText;
    public TextMeshProUGUI totalKillsText;
    public TextMeshProUGUI totalBlocksDestroyed;
    public TextMeshProUGUI totalFruitsCollected;
    public TextMeshProUGUI totalPointsText;

    private void Start()
    {
        startPosition = statsContent.anchoredPosition;

        statsContent.anchoredPosition = startPosition;
        returnToMenuText.SetActive(false);

        DisplayStats();
    }

    void Update()
    {
        if (scrollEnded && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void BeginScrolling()
    {
        if (!scrollStarted)
        {
            scrollStarted = true;
            StartCoroutine(ScrollCredits());
        }
    }

    private IEnumerator ScrollCredits()
    {
        Vector2 finalPosition = new Vector2(startPosition.x, Screen.height + statsContent.sizeDelta.y);

        while (statsContent.anchoredPosition.y < finalPosition.y)
        {
            statsContent.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            yield return null;
        }

        scrollEnded = true;
        yield return new WaitForSeconds(endDelay);
        returnToMenuText.SetActive(true);
    }

    public void DisplayStats()
    {
        var stats = GlobalStatsManager.Instance;

        livesLostText.text = $"Lives Lost: {stats.totalLivesLost}";
        timePlayedText.text = $"Time Played: {stats.totalTime:F2}s";
        yetiKillsText.text = $"Yeti Kills: {stats.totalYetiKills}";
        birdKillsText.text = $"Bird Kills: {stats.totalBirdKills}";
        totalKillsText.text = $"Total Kills: {stats.totalKills}";
        totalPointsText.text = $"Total Points: {stats.totalPoints}";
    }
}
