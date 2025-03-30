using System;
using UnityEngine;

public class Bird_Enemy : MonoBehaviour
{
    float moveSpeed = 0f;
    public int moveTime = 100;
    public Vector3 moveVector;
    float boundX;
    float boundY;
    float spawnY;
    [SerializeField] Boolean isHurt = false;
    private Boolean isOnScreenEdgeX = false;
    private Boolean isOnScreenEdgeY = false;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public String state = "cold";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator.SetBool("isHurt", isHurt);
        animator.SetBool("isColdTemp", false);
        moveVector = new Vector3(1, 0, 0);
        boundX = 9;
        boundY = 6;
        spawnY = this.transform.position.y;

        if (state == "warm") onTempChangeToWarm();
        else if (state == "cold") onTempChangeToCold();
        else if (state == "freezing") onTempChangeToFreezing();
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
            Debug.Log("is on edge x");
            isOnScreenEdgeX = true;
        }
        if ((this.transform.position.y > spawnY+boundY || this.transform.position.y < spawnY-boundY))
        {
            Debug.Log("is on edge y");
            isOnScreenEdgeY = true;
        }
        moveBasedOnState(state);
    }

    void onTempChangeToWarm()
    {
        moveSpeed = 0.008f;
        gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    void onTempChangeToCold()
    {
        animator.SetBool("isColdTemp", true);
        moveSpeed = 0.008f;
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
        float theta = UnityEngine.Random.Range(0, 2* (float)Math.PI);
        float dirX = (float)(2*Math.Cos(theta));
        float dirY = (float)Math.Sin(theta);
        
        moveTime = (int)Math.Abs((UnityEngine.Random.Range(100, 700)*dirX));
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

    void moveBasedOnState(String s)
    {
        if (s == "warm") moveWarm();
        else if (s == "cold") moveCold();
        else if (s == "freezing") moveFreezing();
        else
        {
            moveDead();

        }
    }

    void moveWarm()
    {
        if (isOnScreenEdgeY)
        {
            moveVector.y = (moveVector.y)*-1;
            isOnScreenEdgeY = false;
        }
        if (isOnScreenEdgeX)
        {
            
            float oppositeDirX = -(moveVector.x);
            moveVector = chooseFlyDirectionWarm();
            moveVector.x = oppositeDirX;
            flipSprite(moveVector.x);
            isOnScreenEdgeX = false;
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
        if (isOnScreenEdgeY)
        {
            moveVector.y = (moveVector.y)*-1;
            isOnScreenEdgeY = false;
        }
        if (isOnScreenEdgeX)
        {
            
            float oppositeDirX = -(moveVector.x);
            moveVector = chooseFlyDirectionCold();
            moveVector.x = oppositeDirX;
            flipSprite(moveVector.x);
            isOnScreenEdgeX = false;
        }
        
        this.transform.position += moveVector * moveSpeed;
    }

    void moveFreezing()
    {
        if (isOnScreenEdgeX)
        {
            moveVector *= -1;
            flipSprite(moveVector.x);
            isOnScreenEdgeX = false;
        }
        this.transform.position += moveVector * moveSpeed;
    }

    void moveDead()
    {
        this.transform.position += moveVector * moveSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided with something");
        if (collision.gameObject.tag == "Player"
            && collision.gameObject.transform.position.y > this.transform.position.y)
        {
            moveVector = new Vector3(0, -1, 0);
            isHurt = true;
            animator.SetBool("isHurt", true);
        }
    }
}
