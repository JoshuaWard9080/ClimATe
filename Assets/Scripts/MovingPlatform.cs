using System;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Vector2 currentPosition = new Vector2(1,0);
    public float moveSpeed;
    Boolean playerOnPlatform = false;
    Vector2 previousPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Renderer>().isVisible)
        {//if the object is invisible, flip it's ovement direction

            currentPosition *= -1;
        }
        else
        {
            Debug.Log("Object is now visible");
        }
        move(currentPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D player = collision.gameObject.GetComponent<Rigidbody2D>();
            if (player != null)
            {
                playerOnPlatform = true;
                //to be completed with the addition of player and input manager
            }
        }
    }


    void move(Vector2 movementDirection)
    {
        movementDirection.Normalize();
        float movementX = movementDirection.x * moveSpeed;
        float movementY = movementDirection.y * moveSpeed;
        this.transform.position += new Vector3(movementX,movementY,0);
    }
}
