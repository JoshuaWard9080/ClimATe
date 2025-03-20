using System;
using UnityEngine;

public class FuzzyEnemy : MonoBehaviour
{
    float moveSpeed = 0.003f;
    float jumpSpeed = 2;
    Boolean direction = false; //left = false, right = true;
    int chanceToChangeDir = 15;
    public SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    void move()
    {
        if (UnityEngine.Random.value * chanceToChangeDir == 1)
        {
            changeDirection();
        }
        Vector3 movement = new Vector3(1, 0, 0)*moveSpeed;
        if (!direction) { movement *= -1; }
        this.transform.position += movement;
    }
    Boolean changeDirection()
    {
        spriteRenderer.flipX = direction;
        direction = !direction;
        return direction;
        
    }


}
