using System;
using UnityEngine;

public class Bird_Enemy : MonoBehaviour
{
    float moveSpeed = 0.003f;
    Vector3 moveVector;
    [SerializeField] Boolean isHurt = false;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveVector = new Vector3(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void flipSprite()
    {

    }

    Vector3 chooseFlyDirection()
    {

        return new Vector3(1, 1, 0);
    }

    void moveBasedOnTemperature(String temp)
    {

    }

    void moveWarm()
    {

    }

    void moveCold()
    {

    }

    void moveFreezing()
    {

    }
}
