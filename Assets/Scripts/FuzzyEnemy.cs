using System;
using UnityEngine;
using UnityEngine.UIElements;

public class FuzzyEnemy : MonoBehaviour
{
    float moveSpeed = 0.003f;
    Vector3 moveVector;
    [SerializeField] Boolean isHurt = false;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    [SerializeField] TemperatureManager temperatureManager;

    [Header("Audio")]
    [SerializeField] private AudioSource playerHitYetiAudio;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveVector = new Vector3(1, 0, 0);
        temperatureManager.OnTempChangeToWarm.AddListener(tempChangeToWarm);
        temperatureManager.OnTempChangeToCold.AddListener(tempChangeToCold);
        temperatureManager.OnTempChangeToFreezing.AddListener(tempChangeToFreezing);

        changeDirection();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //pause everything if the pause menu is active
        if (LevelManager.Instance != null && LevelManager.Instance.IsPaused())
        {
            return;
        }

        animator.SetBool("isHurt", isHurt);
        move(moveVector);
    }
    void tempChangeToWarm()
    {
        this.transform.localScale = new Vector3(0.6f, 0.6f, 0);
        moveSpeed = 0.017f;
    }

    void tempChangeToCold()
    {
        this.transform.localScale = new Vector3(0.9f, 0.9f, 0);
        moveSpeed = 0.0135f;
    }

    void tempChangeToFreezing()
    {
        this.transform.localScale = new Vector3(1.5f, 1.5f, 0);
        moveSpeed = 0.01f;
    }
    void move(Vector3 moveVector)
    {
        moveVector.Normalize();

        if (!this.transform.GetChild(0).GetComponent<Renderer>().isVisible)
        {
            //do nothing
        }
        else if (checkIfGoingToFallOffEdge())
        {
            changeDirection();
        }
        else if (checkIfGoingToHitWall())
        {
            changeDirection();
        }
        
        float movementX = moveVector.x * moveSpeed;
        this.transform.position += new Vector3(movementX, 0, 0);
    }
    Boolean checkIfGoingToFallOffEdge()
    {
        float direction = moveVector.x;
        Vector2 raycastStart =
            new Vector2(this.transform.position.x+(direction*(this.transform.localScale.x*0.5f)), this.transform.position.y-(0.5f*this.transform.localScale.y));
        Vector2 raycastDirection = transform.TransformDirection(Vector2.down);
        float maxDistance = 0.3f;
        Debug.DrawRay(raycastStart, raycastDirection* maxDistance);
        RaycastHit2D hit = Physics2D.Raycast(raycastStart, raycastDirection, maxDistance);
        if (hit.collider == null) return true;
        return false;
    }
    Boolean checkIfGoingToHitWall()
    {
        float direction = moveVector.x;
        Vector2 raycastStart =
            new Vector2(this.transform.position.x + (direction * 0.4f *(this.transform.localScale.x+0.3f)), this.transform.position.y);
        Vector2 raycastDirection = transform.TransformDirection(moveVector);
        float maxDistance = 0.1f;
        Debug.DrawRay(raycastStart, raycastDirection * maxDistance);
        RaycastHit2D hit = Physics2D.Raycast(raycastStart, raycastDirection, maxDistance);
        if (isHurt && hit.collider != null && hit.collider.gameObject.CompareTag("BoundaryWalll"))
        {
            respawnYeti();
        }
        if (hit.collider != null && (hit.collider.gameObject.CompareTag("Block") || hit.collider.gameObject.CompareTag("Enemy") || hit.collider.gameObject.CompareTag("BoundaryWalll"))) return hit;
        
        return false;
    }

    void respawnYeti()
    {
        isHurt = false;
        moveSpeed = moveSpeed / 2f;
        Vector3 otherSideOfTheScreen = this.transform.position;
        otherSideOfTheScreen.x *= -0.95f;
        this.transform.position = otherSideOfTheScreen;
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
        this.transform.position += (moveVector)/4;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isHurt && collision.gameObject.CompareTag("Player"))
        {
            //Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            //savedPlayerCollider = collision.collider;
            if (collision.gameObject.transform.position.y > this.transform.position.y + 0.3 && playerHitYetiAudio != null)
            {
                    playerHitYetiAudio.Play();
                
            }
                return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.transform.position.y > this.transform.position.y + 0.3)
            {
    
            if (playerHitYetiAudio != null)
            {
                playerHitYetiAudio.Play();
            }

            isHurt = true;
                moveSpeed = moveSpeed * 2f;
                if (LevelStatsManager.Instance != null)
                {
                    LevelStatsManager.Instance.totalYetiKills++;
                    LevelStatsManager.Instance.totalKills++;
                }
            }
            else  //if yeti hits player
            {
                LivesDisplay lives = FindFirstObjectByType<LivesDisplay>();

                if (lives != null)
                {
                    lives.TakeDamage();
                }
            }
  
        }
    }
}
