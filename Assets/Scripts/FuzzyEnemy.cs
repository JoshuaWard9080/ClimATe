using System;
using UnityEngine;

public class FuzzyEnemy : MonoBehaviour
{
    float moveSpeed = 0.005f;
    Vector3 moveVector;
    float jumpSpeed = 2;
    Boolean isHurt = false;
    public SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveVector = new Vector3(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        move(moveVector);
    }
    void move(Vector3 moveVector)
    {
        moveVector.Normalize();
        float movementX = moveVector.x * moveSpeed;
        this.transform.position += new Vector3(movementX, 0, 0);

        if (!GetComponent<Renderer>().isVisible)
        {
            changeDirection();
        }
        //this.transform.position += moveVector;
        

    }
    void changeDirection()
    {
        moveVector.x *= -1;
        if (moveVector.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        this.transform.position += moveVector;

    }


}
