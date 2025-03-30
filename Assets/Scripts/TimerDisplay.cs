using TMPro;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    // void Awake()
    // {
    //     timeText = GetComponent<TextMeshProUGUI>();
    // }

    // Update is called once per frame
    void Update()
    {
        if (LevelStatsManager.Instance != null && timeText != null)
        {
            float time = LevelStatsManager.Instance.elapsedTime;
            timeText.text = string.Format("{0:00}:{1:00}", Mathf.Floor(time / 60), time % 60);
            //Debug.Log("Time Display Updated: " + timeText.text);
        }
        else
        {
            Debug.LogWarning("TimerDisplay can't find LevelStatsManager Instance");
        }
    }
}
