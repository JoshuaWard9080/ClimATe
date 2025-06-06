using System;
using UnityEngine;
using UnityEngine.UIElements;

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
    private String state = "warm";
    [SerializeField] TemperatureManager temperatureManager;

    [Header("Audio")]
    [SerializeField] private AudioSource flappingAudio;
    [SerializeField] private AudioSource walkingAudio;
    [SerializeField] private AudioSource playerHitsBirdAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator.SetBool("isHurt", isHurt);
        animator.SetBool("isColdTemp", false);

        moveVector = new Vector3(1, 0, 0);
        boundX = 8;
        boundY = 6;
        spawnY = this.transform.position.y;

        if (state == "warm") tempChangeToWarm();
        else if (state == "cold") tempChangeToCold();
        else if (state == "freezing")
        {
            animator.SetBool("isColdTemp", true);
            tempChangeToFreezing();
        }

        temperatureManager.OnTempChangeToWarm.AddListener(tempChangeToWarm);
        temperatureManager.OnTempChangeToCold.AddListener(tempChangeToCold);
        temperatureManager.OnTempChangeToFreezing.AddListener(tempChangeToFreezing);
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.transform.GetChild(0).GetComponent<Renderer>().isVisible)
        {
            return;
        }
        if (LevelManager.Instance != null && LevelManager.Instance.IsPaused())
        {
            return;
        }

        if (isHurt)
        {
            moveDead();
        }
        else
        {
        if (state == "freezing")
            {
                if (walkingAudio != null && !walkingAudio.isPlaying)
                {
                    walkingAudio.Play();
                }

                if (flappingAudio != null && flappingAudio.isPlaying)
                {
                    flappingAudio.Stop();
                }
            }
            else if (state == "warm" || state == "cold")
            {
                if (flappingAudio != null && !flappingAudio.isPlaying)
                {
                    flappingAudio.Play(); //make sure audio has loop = true in the inspector
                }

                if (walkingAudio != null && walkingAudio.isPlaying)
                {
                    walkingAudio.Stop();
                }
            }

            if ((this.transform.position.x > boundX || this.transform.position.x < -boundX))
            {
                isOnScreenEdgeX = true;
            }

            if ((this.transform.position.y > spawnY + boundY || this.transform.position.y < spawnY - boundY))
            {
                isOnScreenEdgeY = true;
            }

            moveBasedOnState(state);
        }
    }

    void tempChangeToWarm()
    {
        animator.SetBool("isColdTemp", false);
        moveSpeed = 0.016f;
        gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    void tempChangeToCold()
    {
        animator.SetBool("isColdTemp", false);
        moveSpeed = 0.016f;
        gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    void tempChangeToFreezing()
    {
        animator.SetBool("isColdTemp", true);
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
        float dirY = (float)(Math.Sin(theta)/2);
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
        float randomShake = UnityEngine.Random.Range(-moveSpeed*5,moveSpeed*5);
        Vector3 newPosition = (moveVector * moveSpeed);
        newPosition.x += randomShake;
        this.transform.position += newPosition;
    }

    void moveDead()
    {
        this.transform.position += moveVector * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            if (collision.gameObject.transform.position.y > this.transform.position.y + 0.3)
            {
                moveVector = new Vector3(0, -1, 0);
                //dead, stop all sound
                if (flappingAudio != null && flappingAudio.isPlaying)
                {
                    flappingAudio.Stop();
                }

                if (walkingAudio != null && walkingAudio.isPlaying)
                {
                    walkingAudio.Stop();
                }
                isHurt = true;
                animator.SetBool("isHurt", true);
                gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());

                if (playerHitsBirdAudio != null)
            {
                playerHitsBirdAudio.Play(); //make sure loop does NOT equal true in inspector
            }

                if (LevelStatsManager.Instance != null)
                {
                    LevelStatsManager.Instance.totalBirdKills++;
                    LevelStatsManager.Instance.totalKills++;
                }

            }
            else //bird hits player
            {
                LivesDisplay lives = FindFirstObjectByType<LivesDisplay>();

                if (lives != null)
                {
                    lives.TakeDamage();
                }
            }
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            isOnScreenEdgeX = true;
        }
    }
}
