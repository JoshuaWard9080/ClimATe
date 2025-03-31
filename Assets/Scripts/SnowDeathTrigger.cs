using UnityEngine;

public class SnowDeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PilingSnow"))
        {
            Debug.Log("Player has been buried by the forbidden snow. Instant death.");

            var statsManager = LevelStatsManager.Instance;

            if (statsManager != null)
            {
                statsManager.remainingLives = 0;
                statsManager.totalLivesLost++;

                var lives = FindObjectOfType<LivesDisplay>();

                if (lives != null)
                {
                    lives.PlayerDied();
                }
            }
        }
    }
}
