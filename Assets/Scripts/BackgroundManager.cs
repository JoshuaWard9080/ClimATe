using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform Sky;
    [SerializeField] private Transform Clouds;
    [SerializeField] private Transform Mountains;
    [SerializeField] private Transform Midground;
    [SerializeField] private Transform Foreground;
    [SerializeField] private float speed;
    private Transform CloudsFirst;
    private Transform CloudsSecond;
    private Transform IntitialCloudPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y, 0);
        CloudsFirst = Clouds.GetChild(0);
        CloudsSecond = Clouds.GetChild(1);
        IntitialCloudPos = CloudsFirst.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaPosition = calculateChangeInCameraPosition();
        transform.position += deltaPosition;
        Clouds.position -= (deltaPosition / 40);
        Mountains.position -= (deltaPosition /16);
        Midground.position -= (deltaPosition/8);
        Foreground.position -= (deltaPosition/5);
        moveClouds();
    }

    Vector3 calculateChangeInCameraPosition()
    {
        Vector3 newPosition = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y, 0);
        return newPosition - transform.position;
    }

    void moveClouds()
    {
        float moveAmount = speed * Time.deltaTime;

        CloudsFirst.transform.position -= new Vector3(moveAmount, 0,0);
        CloudsSecond.transform.position -= new Vector3(moveAmount, 0, 0);
        
    }
}
