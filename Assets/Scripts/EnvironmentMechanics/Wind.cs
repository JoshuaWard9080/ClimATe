using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private float windStrength; //warm = 0, standard = 1, freezing = 3
    [SerializeField] private float windSpeed; //warm = 0, standard = 1, freezing = 3
    [SerializeField] private Vector2 windDirection;

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
}
