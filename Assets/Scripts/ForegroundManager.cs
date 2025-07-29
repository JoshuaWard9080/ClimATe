using UnityEngine;

public class ForegroundManager : MonoBehaviour
{
    [SerializeField] private Transform Clouds;
    [SerializeField] private float speed;
    [SerializeField] TemperatureManager temperatureManager;
    [SerializeField] private Transform CloudsFirst;
    [SerializeField] private Transform CloudsSecond;
    private float cloudAnchor = -20f;
    private float cloudRespawn = 15f;

    private float coldSpeed = 2f;
    private float freezingSpeed = 4f;
    private float warmSpeed = 0.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CloudsFirst = Clouds.GetChild(0);
        CloudsSecond = Clouds.GetChild(1);
        temperatureManager.OnTempChangeToCold.AddListener(tempChangeToCold);
        temperatureManager.OnTempChangeToFreezing.AddListener(tempChangeToFreezing);
        temperatureManager.OnTempChangeToWarm.AddListener(tempChangeToWarm);
    }

    // Update is called once per frame
    void Update()
    {
        float moveAmount = speed * Time.deltaTime;

        CloudsFirst.transform.position -= new Vector3(moveAmount, 0, 0);
        CloudsSecond.transform.position -= new Vector3(moveAmount, 0, 0);

        if (CloudsFirst.transform.position.x < cloudAnchor)
        {
            CloudsFirst.transform.position = new Vector3(cloudRespawn, CloudsFirst.transform.position.y, -1);
        }
        if (CloudsSecond.transform.position.x < cloudAnchor)
        {
            CloudsSecond.transform.position = new Vector3(cloudRespawn, CloudsSecond.transform.position.y, -1);
        }
    }

    void tempChangeToCold()
    {
        changeSpeed(coldSpeed);
    }

    void tempChangeToWarm()
    {
        changeSpeed(warmSpeed);
    }

    void tempChangeToFreezing()
    {
        changeSpeed(freezingSpeed);
    }

    void changeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
