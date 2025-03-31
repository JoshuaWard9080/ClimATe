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

    [SerializeField] private TextAnimation textAnimator;
    [SerializeField] private TextMeshProUGUI[] animatedStatsTexts;

    //points/stats
    [SerializeField] private TextMeshProUGUI globalPointsText;
    [SerializeField] private TextMeshProUGUI globalKillsText;
    [SerializeField] private TextMeshProUGUI globalTimeText;
    [SerializeField] private TextMeshProUGUI globalLivesLostText;

    private void Start()
    {
        startPosition = statsContent.anchoredPosition;

        statsContent.anchoredPosition = startPosition;
        returnToMenuText.SetActive(false);

        DisplayFianlStats();

        if (textAnimator != null)
        {
            textAnimator.SetTexts(animatedStatsTexts);
            StartCoroutine(textAnimator.StartAnimation());
        }
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

    void DisplayFianlStats()
    {
        if (GlobalStatsManager.Instance == null)
        {
            Debug.LogError("GlobalStatsManager is null in VictoryScene");
            return;
        }

        var g = GlobalStatsManager.Instance;

        globalPointsText.text = $"Total Points: {g.totalPoints}";
        globalKillsText.text = $"Total Kills: {g.totalKills}";
        globalTimeText.text = $"Total Time: {g.totalTime:F1}s";
        globalLivesLostText.text = $"Lives Lost: {g.totalLivesLost}";
    }
}
