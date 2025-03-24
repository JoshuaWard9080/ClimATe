using UnityEngine;

public class PilingSnow : MonoBehaviour
{
    [SerializeField] private float snowSpeed = 1f;

    void Update()
    {
        transform.position += Vector3.up * snowSpeed * Time.deltaTime;
    }

    public void SetSnowSpeed(float newSnowSpeed)
    {
        snowSpeed = newSnowSpeed;
        Debug.Log("Snow piling speed set to: " + newSnowSpeed);
    }
}
