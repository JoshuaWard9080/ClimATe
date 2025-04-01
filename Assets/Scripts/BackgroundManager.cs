using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform Sky;
    [SerializeField] private Transform Clouds;
    [SerializeField] private Transform Mountains;
    [SerializeField] private Transform Midground;
    [SerializeField] private Transform Foreground;
    private Transform CloudsFirst;
    private Transform CloudsSecond;
    private float cloudAnchor;
    private float cloudRespawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y, 0);
        CloudsFirst = Clouds.GetChild(0);
        CloudsSecond = Clouds.GetChild(1);
        cloudAnchor = CloudsFirst.transform.position.x;
        cloudRespawn = CloudsSecond.transform.position.x;

        Midground.position += new Vector3(0, 1, 0);
        Foreground.position += new Vector3(0,1,0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaPosition = calculateChangeInCameraPosition();
        transform.position += deltaPosition;
        Clouds.position -= (deltaPosition / 20);
        Mountains.position -= (deltaPosition /10);
        Midground.position -= (deltaPosition/6);
        Foreground.position -= (deltaPosition/3);
        moveClouds();
    }

    Vector3 calculateChangeInCameraPosition()
    {
        Vector3 newPosition = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y, 0);
        return newPosition - transform.position;
    }

    void moveClouds()
    {
        float moveAmount = 0.6f * Time.deltaTime;

        CloudsFirst.transform.position -= new Vector3(moveAmount, 0,0);
        CloudsSecond.transform.position -= new Vector3(moveAmount, 0, 0);
        if (CloudsSecond.transform.position.x < cloudAnchor)
        {
            CloudsFirst.transform.position = CloudsSecond.transform.position;
            CloudsSecond.transform.position = new Vector3(cloudRespawn, CloudsSecond.transform.position.y,0);    
        }
        
    }
}
