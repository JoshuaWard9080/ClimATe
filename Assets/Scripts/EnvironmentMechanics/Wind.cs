using UnityEngine;

public class Wind : MonoBehaviour
{
    public void SetWindStrength(float windStrength)
    {
        Debug.Log("Wind strength set to: " + windStrength);
    }

    public void SetWindSpeed(float windSpeed)
    {
        Debug.Log("Wind speed set to: " + windSpeed);
    }
}
