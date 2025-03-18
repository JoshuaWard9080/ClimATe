using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = new Vector2(1, 0);
        if (!GetComponent<Renderer>().isVisible)
        {//if the object is invisible, flip it's ovement direction
            Debug.Log("Object is now invisible");
            moveVector *= -1;
        }
        else
        {
            Debug.Log("Object is now visible");
        }
        move(moveVector);
    }

    

    void move(Vector2 movementDirection)
    {
        movementDirection.Normalize();
        float movementX = movementDirection.x * moveSpeed;
        float movementY = movementDirection.y * moveSpeed;
        this.transform.position += new Vector3(movementX,movementY,0);
    }
}
