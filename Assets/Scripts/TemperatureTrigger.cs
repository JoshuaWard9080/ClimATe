using UnityEngine;

public class TemperatureTrigger : MonoBehaviour
{
    [SerializeField] private TemperatureState tempState;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        var tempMan = FindObjectOfType<TemperatureManager>();
        if (tempMan != null)
        {
            tempMan.setTemp(tempState);
        }
    }
}
