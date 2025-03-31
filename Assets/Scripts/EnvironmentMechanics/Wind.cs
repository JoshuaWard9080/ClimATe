using UnityEditor;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private float windStrength; //warm = 0, standard = 3, freezing = 5
    [SerializeField] private float windSpeed; //warm = 0, standard = 3, freezing = 5
    [SerializeField] private float windDirection; // 0 for blowing from the left, 180 for blowing from the right
    [SerializeField] TemperatureManager temperatureManager;
    AreaEffector2D effector;

    private float coldWindStrength = 3f;
    private float warmWindStrength = 0f;
    private float freezingWindStrength = 5f;

    public void Start()
    {
        effector = this.GetComponent<AreaEffector2D>();
        temperatureManager.OnTempChangeToWarm.AddListener(tempChangeToWarm);
        temperatureManager.OnTempChangeToCold.AddListener(tempChangeToCold);
        temperatureManager.OnTempChangeToFreezing.AddListener(tempChangeToFreezing);
    }

    void tempChangeToWarm()
    {
        SetWindSpeed(warmWindStrength);
    }

    void tempChangeToCold()
    {
        SetWindSpeed(coldWindStrength);
    }

    void tempChangeToFreezing()
    {
        SetWindSpeed(freezingWindStrength);
    }

    public void SetWindSpeed(float newWindSpeed)
    {
        windSpeed = newWindSpeed;
        effector.forceMagnitude = windSpeed;
        Debug.Log("Wind speed set to: " + newWindSpeed);
    }
}
