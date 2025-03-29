using UnityEngine;

public class PilingSnow : MonoBehaviour
{
    [SerializeField] private float snowSpeed = 1f;
    private bool isRising = false;


    void Update()
    {
        if (!isRising)
        {
            return;
        }

        //start rising
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
}
