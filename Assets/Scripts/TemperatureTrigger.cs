using UnityEngine;
using UnityEngine.Events;

public class TemperatureTrigger : MonoBehaviour
{
    [SerializeField] private TemperatureState tempState;
    [SerializeField] private TemperatureManager tman;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (tempState == TemperatureState.Warm) tman.OnTempChangeToWarm?.Invoke();
            if (tempState == TemperatureState.Cold) tman.OnTempChangeToCold?.Invoke();
            if (tempState == TemperatureState.Freezing) tman.OnTempChangeToFreezing?.Invoke();
        }

        
    }
}
