using System;
using UnityEngine;

public class Bird_Enemy : MonoBehaviour
{
    float moveSpeed = 0.010f;
    public int moveTime = 100;
    public Vector3 moveVector;
    float boundX;
    [SerializeField] Boolean isHurt = false;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveVector = new Vector3(1, 0, 0);
        boundX = 9;
    }

    // Update is called once per frame
    void Update()
    {
        moveBasedOnTemperature("warm");
    }

    void flipSprite()
    {

    }

    Vector3 chooseFlyDirection()
    {
        
        float theta = UnityEngine.Random.Range(0, 2*3.14159f);
        float dirX = (float)Math.Cos(theta);
        float dirY = (float)Math.Sin(theta);
        
        moveTime = (int)Math.Abs((UnityEngine.Random.Range(100, 800)*dirX));
        return new Vector3(dirX, dirY, 0);
    }

    void moveBasedOnTemperature(String temp)
    {
        if (temp == "warm") moveWarm();
        else if (temp == "cold") moveCold();
        else if (temp == "freezing") moveFreezing();
        else
        {
            Debug.Log("Invalid temperature for bird");

        }
    }

    void moveWarm()
    {
        if (this.transform.position.x > boundX || this.transform.position.x < -boundX)
        {
            float oppositeDirX = -(moveVector.x);
            moveVector = chooseFlyDirection();
            moveVector.x = oppositeDirX;

        }
        if (moveTime <= 0)
        {
            moveVector = chooseFlyDirection();
        }
        moveTime--;
        this.transform.position += moveVector * moveSpeed;

    }

    void moveCold()
    {

    }

    void moveFreezing()
    {

    }
}
