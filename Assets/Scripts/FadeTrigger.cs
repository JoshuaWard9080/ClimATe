using UnityEngine;

public class FadeTrigger : MonoBehaviour
{
    //should open LevelComplete scene
    public string targetSceneName = "LevelComplete";

    //when player jumps into the trigger, it fades 
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by Player");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Fading...");

            //gets the CloudFadeUI object, calls the Fade function, then loads the target scene, in this case LevelComplete
            var fade = FindObjectOfType<CloudFadeUI>();
            fade.FadeIn(() =>
            {
                Debug.Log("Loading scene now...");

                SceneLoader.Instance.LoadScene(targetSceneName);
            });
        }
        else
        {
            Debug.LogWarning("CloudFadeUI not found");
        }
    }
}
