using UnityEditor;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private float windStrength; //warm = 0, standard = 1, freezing = 3
    [SerializeField] private float windSpeed; //warm = 0, standard = 1, freezing = 3
    [SerializeField] private Vector2 windDirection;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] TemperatureManager temperatureManager;

    [SerializeField] private float drag = 10.0f;
    public void Start()
    {
        temperatureManager.OnTempChangeToWarm.AddListener(tempChangeToWarm);
        temperatureManager.OnTempChangeToCold.AddListener(tempChangeToCold);
        temperatureManager.OnTempChangeToFreezing.AddListener(tempChangeToFreezing);
    }

    void tempChangeToWarm()
    {
        SetWindStrength(0f);
        SetWindSpeed(0f);
    }

    void tempChangeToCold()
    {
        SetWindStrength(1f);
        SetWindSpeed(1f);
    }

    void tempChangeToFreezing()
    {
        SetWindStrength(3f);
        SetWindSpeed(3f);
    }

    public void SetWindStrength(float newWindStrength)
    {
        windStrength = newWindStrength;
        Debug.Log("Wind strength set to: " + newWindStrength);
    }

    public void SetWindSpeed(float newWindSpeed)
    {
        windSpeed = newWindSpeed;
        Debug.Log("Wind speed set to: " + newWindSpeed);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D objectRB = collision.GetComponent<Rigidbody2D>();
        objectRB.AddForce(new Vector2(-(objectRB.linearVelocity.x * drag), 0));

    }
}
