using UnityEngine;

public class LevelCompleteTrigger : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered)
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            triggered = true;
            LevelManager.Instance.LevelCompleteTransition();
        }
    }
}
