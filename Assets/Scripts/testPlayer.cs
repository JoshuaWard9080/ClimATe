using UnityEngine;

public class testPlayer : MonoBehaviour
{
    public float speed = 2;
    public Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.transform.position += new Vector3(-speed, 0,0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.transform.position += new Vector3(speed, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            rb.AddForce(400*Vector2.up);
        }

        
    }
}
