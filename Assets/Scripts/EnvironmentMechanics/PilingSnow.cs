using UnityEngine;

public class PilingSnow : MonoBehaviour
{
    [SerializeField] private float snowSpeed = 0.05f;
    [SerializeField] private TemperatureManager temperatureManager;
    private bool isRising = false;

    private float coldSnowSpeed = 0.06f;
    private float freezingSnowSpeed = 0.12f;
    private float warmSnowSpeed = 0.03f;

    private void Start()
    {
        temperatureManager.OnTempChangeToCold.AddListener(SetCold);
        temperatureManager.OnTempChangeToFreezing.AddListener(SetFreezing);
        temperatureManager.OnTempChangeToWarm.AddListener(SetWarm);
    }


    void FixedUpdate()
    {
        if (!isRising)
        {
            return;
        }

        //start rising
        //pause everything if the pause menu is active
        if (LevelManager.Instance.IsPaused() && LevelManager.Instance != null)
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
