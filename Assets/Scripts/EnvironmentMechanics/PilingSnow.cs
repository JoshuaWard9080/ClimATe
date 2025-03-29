using UnityEngine;

public class PilingSnow : MonoBehaviour
{
    [SerializeField] private float snowSpeed = 1f;
    [SerializeField] private float delayBeforeRising = 8f;

    private float startTime;
    private bool startedRising = false;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (!startedRising)
        {
            if (Time.time >= startTime + delayBeforeRising)
            {
                startedRising = true;
            }
            else
            {
                return;
            }
        }

        //start rising
        transform.position += Vector3.up * snowSpeed * Time.deltaTime;
    }

    public void SetSnowSpeed(float newSnowSpeed)
    {
        snowSpeed = newSnowSpeed;
        Debug.Log("Snow piling speed set to: " + newSnowSpeed);
    }
}
