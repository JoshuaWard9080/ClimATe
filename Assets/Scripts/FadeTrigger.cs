using UnityEngine;

public class FadeTrigger : MonoBehaviour
{
    //should open LevelComplete scene
    public string targetSceneName = "LevelComplete";

    //when player jumps into the trigger, it fades 
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Fading...");

            var fade = FindFirstObjectByType<CloudFadeUI>();
            if (fade != null)
            {
                fade.FadeIn(() =>
                {
                    Debug.Log("Calling CompleteLevel via FadeTrigger...");
                    LevelManager.Instance.CompleteLevel();
                });
            }
            else
            {
                Debug.LogWarning("CloudFadeUI not found");
            }
        }
    }
}
