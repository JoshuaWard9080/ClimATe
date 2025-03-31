using System;
using Unity.VisualScripting;
using UnityEngine;

public class FuzzyEnemy : MonoBehaviour
{
    float moveSpeed = 0.003f;
    Vector3 moveVector;
    [SerializeField] Boolean isHurt = false;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    [SerializeField] TemperatureManager temperatureManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveVector = new Vector3(1, 0, 0);
        temperatureManager.OnTempChangeToWarm.AddListener(tempChangeToWarm);
        temperatureManager.OnTempChangeToCold.AddListener(tempChangeToCold);
        temperatureManager.OnTempChangeToFreezing.AddListener(tempChangeToFreezing);

        
    }

    // Update is called once per frame
    void Update()
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
        PushBlocks(false);
    }

    void tempChangeToCold()
    {
        PushBlocks(true);
    }

    void tempChangeToFreezing()
    {
        PushBlocks(true);
    }
    void move(Vector3 moveVector)
    {
        moveVector.Normalize();
        
        if (!this.transform.GetChild(0).GetComponent<Renderer>().isVisible)
        {
            manageOffScreen();
        }
        if (!checkIfGoingToFallOffEdge())
        {
            changeDirection();
        }
        if (checkIfGoingToHitWall())
        {
            changeDirection();
        }
        float movementX = moveVector.x * moveSpeed;
        this.transform.position += new Vector3(movementX, 0, 0);
    }
    void manageOffScreen()
    {
        if (!this.transform.GetChild(0).GetComponent<Renderer>().isVisible)
        {
            if (isHurt)
            {
                isHurt = false;
                Vector3 otherSideOfTheScreen = this.transform.position;
                otherSideOfTheScreen.x *= -1;
                this.transform.position = otherSideOfTheScreen;
            }
            else
            {
                changeDirection();
            }

        }
    }
    Boolean checkIfGoingToFallOffEdge()
    {
        float direction = moveVector.x;
        Vector2 raycastStart =
            new Vector2(this.transform.position.x+(direction*0.3f), this.transform.position.y-0.2f);
        Vector2 raycastDirection = transform.TransformDirection(Vector2.down);
        float maxDistance = 0.2f;
        Debug.DrawRay(raycastStart, raycastDirection* maxDistance);
        RaycastHit2D hit = Physics2D.Raycast(raycastStart, raycastDirection, maxDistance);
        return hit;
    }
    Boolean checkIfGoingToHitWall()
    {
        float direction = moveVector.x;
        Vector2 raycastStart =
            new Vector2(this.transform.position.x + (direction * 0.4f), this.transform.position.y);
        Vector2 raycastDirection = transform.TransformDirection(moveVector);
        float maxDistance = 0.1f;
        Debug.DrawRay(raycastStart, raycastDirection * maxDistance);
        RaycastHit2D hit = Physics2D.Raycast(raycastStart, raycastDirection, maxDistance);
        if (hit.collider == null || (hit.collider.gameObject.tag == "Player") || (hit.collider.gameObject.tag == "Trigger")) return false;
        //if (hit.collider != null)Debug.Log("going to hit something: " + hit.collider.gameObject);
        //if (hit.collider != null) Debug.Log("going to hit something: " + hit.collider.gameObject.tag);
        return hit;
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
        this.transform.position += (moveVector/4);

    }

    public void PushBlocks(bool canPush)
    {
        Debug.Log("Fuzzy enemy can push blocks: " + canPush);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"
            && collision.gameObject.transform.position.y > this.transform.position.y + 0.3)
        {
            isHurt = true;

            if (LevelStatsManager.Instance != null)
            {
                LevelStatsManager.Instance.totalYetiKills++;
                LevelStatsManager.Instance.totalKills++;
                Debug.Log("A yeti has been killed... is this morally okay? He's just a chill guy :(");
            }
            else
            {
                Debug.LogWarning("No LevelStatsManager Instance in FuzzyEnemy script");
            }
        }
    }
}
