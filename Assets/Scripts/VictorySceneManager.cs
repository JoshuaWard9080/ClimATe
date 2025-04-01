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
    public TextMeshProUGUI totalIciclesDestroyed;
    public TextMeshProUGUI totalFishCollected;
    public TextMeshProUGUI timeBonus;
    public TextMeshProUGUI totalPointsText;

    [Header("Audio")]
    [SerializeField] private AudioSource victorySceneAudio;

    private void Start()
    {
        //play music
        if (victorySceneAudio != null)
        {
            victorySceneAudio.Play();
        }

        startPosition = statsContent.anchoredPosition;

        statsContent.anchoredPosition = startPosition;
        returnToMenuText.SetActive(false);

        DisplayStats();

        var textAnimator = FindObjectOfType<TextAnimation>();
        if (textAnimator != null)
        {
            textAnimator.SetTexts(new TextMeshProUGUI[] {
                livesLostText,
                timePlayedText,
                yetiKillsText,
                birdKillsText,
                totalKillsText,
                totalBlocksDestroyed,
                totalIciclesDestroyed,
                totalFishCollected,
                timeBonus,
                totalPointsText
            });

            // foreach (var txt in textAnimator.GetTexts())
            // {
            //     Debug.Log($"Assigned text: {txt?.name ?? "NULL"}");
            // }

            StartCoroutine(textAnimator.StartAnimation());
        }
        else
        {
            Debug.LogError("TextAnimation script not found in VictoryScene!");
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

    public void DisplayStats()
    {
        var stats = GlobalStatsManager.Instance;

        livesLostText.text = $"Lives Lost: {stats.totalLivesLost}";
        timePlayedText.text = $"Time Played: {stats.totalTime:F2}s";
        yetiKillsText.text = $"Yeti Kills: {stats.totalYetiKills}";
        birdKillsText.text = $"Bird Kills: {stats.totalBirdKills}";
        totalKillsText.text = $"Total Kills: {stats.totalBirdKills + stats.totalYetiKills}";
        totalBlocksDestroyed.text = $"Blocks Destroyed: {stats.blocksDestroyed}";
        totalIciclesDestroyed.text = $"Icicles Destroyed: {stats.iciclesDestroyed}";
        totalFishCollected.text = $"Fish Collected: {stats.fishCollected}";
        timeBonus.text = $"Time Bonus: {stats.timeBonus}";
        totalPointsText.text = $"Total Points: {stats.totalPoints}";

        TextMeshProUGUI[] statTexts = new TextMeshProUGUI[]
        {
            livesLostText,
            timePlayedText,
            yetiKillsText,
            birdKillsText,
            totalKillsText,
            totalBlocksDestroyed,
            totalIciclesDestroyed,
            totalFishCollected,
            timeBonus,
            totalPointsText
        };

        var anim = GetComponent<TextAnimation>();
        if (anim != null)
        {
            anim.SetTexts(statTexts);
            StartCoroutine(anim.StartAnimation());
        }
        else
        {
            Debug.LogError("TextAnimation not found on VictoryManager");
        }
    }
}
