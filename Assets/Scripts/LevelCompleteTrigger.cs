using UnityEngine;

public class LevelCompleteTrigger : MonoBehaviour
{
    private bool triggered = false;

    private void Oter2D(Collider2D collision)
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
