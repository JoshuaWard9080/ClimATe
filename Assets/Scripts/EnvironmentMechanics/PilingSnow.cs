using UnityEngine;

public class PilingSnow : MonoBehaviour
{
    [SerializeField] private float snowSpeed = 1f;

    void Update()
    {
        //pause everything if the pause menu is active
        if (LevelManager.Instance != null && LevelManager.Instance.IsPaused())
        {
            return;
        }
        
        transform.position += Vector3.up * snowSpeed * Time.deltaTime;
    }

    public void SetSnowSpeed(float newSnowSpeed)
    {
        snowSpeed = newSnowSpeed;
        Debug.Log("Snow piling speed set to: " + newSnowSpeed);
    }
}
