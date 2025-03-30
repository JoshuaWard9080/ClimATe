using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Transform Sky;
    [SerializeField] private Transform Clouds;
    [SerializeField] private Transform Mountains;
    [SerializeField] private Transform Midground;
    [SerializeField] private Transform Foreground;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform = camera.transform;
    }
}
