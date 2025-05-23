using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private float windSpeed; //warm = 0, standard = 0.7, freezing = 1.5
    [SerializeField] private float windDirection; // 0 for blowing from the left, 180 for blowing from the right
    [SerializeField] TemperatureManager temperatureManager;
    AreaEffector2D effector;

    private float coldWindSpeed = 0.7f;
    private float warmWindSpeed = 0f;
    private float freezingWindSpeed = 1.5f;

    public void Start()
    {
        effector = this.GetComponent<AreaEffector2D>();
        temperatureManager.OnTempChangeToWarm.AddListener(tempChangeToWarm);
        temperatureManager.OnTempChangeToCold.AddListener(tempChangeToCold);
        temperatureManager.OnTempChangeToFreezing.AddListener(tempChangeToFreezing);
    }

    void tempChangeToWarm()
    {
        SetWindSpeed(warmWindSpeed);
    }

    void tempChangeToCold()
    {
        SetWindSpeed(coldWindSpeed);
    }

    void tempChangeToFreezing()
    {
        SetWindSpeed(freezingWindSpeed);
    }

    public void SetWindSpeed(float newWindSpeed)
    {
        windSpeed = newWindSpeed;
        effector.forceMagnitude = windSpeed;
        Debug.Log("Wind speed set to: " + newWindSpeed);
    }
}
