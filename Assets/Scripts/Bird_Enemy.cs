using System;
using UnityEngine;

public class Bird_Enemy : MonoBehaviour
{
    float moveSpeed = 0f;
    public int moveTime = 100;
    public Vector3 moveVector;
    float boundX;
    [SerializeField] Boolean isHurt = false;
    private Boolean isOnScreenEdge = false;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    [SerializeField]  public String temperature = "warm";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveVector = new Vector3(1, 0, 0);
        boundX = 9;
        
        if (temperature == "warm") onTempChangeToWarm();
        else if (temperature == "cold") onTempChangeToCold();
        else if (temperature == "freezing") onTempChangeToFreezing();
        else
        {
            Debug.Log("Invalid temperature for bird in start method");

        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.transform.position.x > boundX || this.transform.position.x < -boundX))
        {
            isOnScreenEdge = true;
        }
        moveBasedOnTemperature(temperature);
    }

    void onTempChangeToWarm()
    {
        moveSpeed = 0.008f;
        gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    void onTempChangeToCold()
    {
        moveSpeed = 0.03f;
        gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    void onTempChangeToFreezing()
    {
        moveSpeed = 0.005f;
        moveVector = new Vector3(1, 0, 0);
        gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
    void flipSprite(float dir)
    {
        if (dir > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    Vector3 chooseFlyDirectionWarm()
    {
        float theta = UnityEngine.Random.Range(0, 2*3.14159f);
        float dirX = (float)(2*Math.Cos(theta));
        float dirY = (float)Math.Sin(theta);
        
        moveTime = (int)Math.Abs((UnityEngine.Random.Range(100, 1000)*dirX));
        flipSprite(dirX);
        return new Vector3(dirX, dirY, 0);
    }

    Vector3 chooseFlyDirectionCold()
    {
        float theta = UnityEngine.Random.Range((float)-(Math.PI/8),(float)Math.PI/8);
        float dirX = 30;
        float dirY = (float)Math.Sin(theta);
        flipSprite(dirX);

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
        if (isOnScreenEdge)
        {
            flipSprite(moveVector.x);
            float oppositeDirX = -(moveVector.x);
            moveVector = chooseFlyDirectionWarm();
            moveVector.x = oppositeDirX;
            isOnScreenEdge = false;
        }
        if (moveTime <= 0)
        {
            moveVector = chooseFlyDirectionWarm();
        }
        moveTime--;
        this.transform.position += moveVector * moveSpeed;

    }

    void moveCold()
    {
        if (isOnScreenEdge)
        {
            flipSprite(moveVector.x);
            float oppositeDirX = -(moveVector.x);
            moveVector = chooseFlyDirectionCold();
            moveVector.x = oppositeDirX;
            isOnScreenEdge = false;
        }
        this.transform.position += moveVector * moveSpeed;
    }

    void moveFreezing()
    {
        if (isOnScreenEdge)
        {
            moveVector *= -1;
            flipSprite(moveVector.x);
            isOnScreenEdge = false;
        }
        this.transform.position += moveVector * moveSpeed;
    }
}
