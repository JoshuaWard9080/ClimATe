using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] public Rigidbody2D player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
        Vector3 cameraPosition = new Vector3(0, playerY, -10);
        this.transform.position = cameraPosition;
    }
}
