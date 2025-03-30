using System;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Vector2 currentPosition = new Vector2(1,0);
    public float moveSpeed;
    Boolean playerOnPlatform = false;
    Vector2 previousPosition;
    Renderer sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = transform.GetChild(0).GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //pause everything if the pause menu is active
        if (LevelManager.Instance != null && LevelManager.Instance.IsPaused())
        {
            return;
        }

        if (!sprite.isVisible)
        {//if the object is invisible, flip it's ovement direction

            currentPosition *= -1;
        }
        move(currentPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var playerMovement = collision.collider.GetComponent<PlayerMovement>();
        if (playerMovement != null && collision.rigidbody.transform.position.y > this.transform.position.y + 0.23)
        {
            playerMovement.SetParent(transform);
        }


        //if (collision.gameObject.tag == "Player")
        //{
        //    Rigidbody2D player = collision.gameObject.GetComponent<Rigidbody2D>();
        //    if (player != null)
        //    {
        //        playerOnPlatform = true;
        //        //to be completed with the addition of player and input manager
        //    }
        //}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var playerMovement = collision.collider.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.ResetParent();
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
