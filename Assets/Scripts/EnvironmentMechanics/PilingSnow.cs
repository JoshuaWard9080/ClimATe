using UnityEngine;

public class PilingSnow : MonoBehaviour
{
    [SerializeField] private float snowSpeed = 0.05f;
    [SerializeField] private TemperatureManager temperatureManager;
    private bool isRising = false;

    private float coldSnowSpeed = 0.05f;
    private float freezingSnowSpeed = 0.5f;
    private float warmSnowSpeed = 0.01f;

    private void Start()
    {
        temperatureManager.OnTempChangeToCold.AddListener(SetCold);
        temperatureManager.OnTempChangeToFreezing.AddListener(SetFreezing);
        temperatureManager.OnTempChangeToWarm.AddListener(SetWarm);
    }


    void Update()
    {
        if (!isRising)
        {
            return;
        }

        //start rising
        //pause everything if the pause menu is active
        if (LevelManager.Instance != null && LevelManager.Instance.IsPaused())
        {
            return;
        }

        transform.position += Vector3.up * snowSpeed * Time.deltaTime;
    }

    public void StartRising()
    {
        isRising = true;
    }

    public void SetSnowSpeed(float newSnowSpeed)
    {
        snowSpeed = newSnowSpeed;
        Debug.Log("Snow piling speed set to: " + newSnowSpeed);
    }

    public void SetFreezing()
    {
        SetSnowSpeed(freezingSnowSpeed);
    }

    public void SetCold()
    {
        SetSnowSpeed(coldSnowSpeed);
    }

    public void SetWarm()
    {
        SetSnowSpeed(warmSnowSpeed);
    }
}
