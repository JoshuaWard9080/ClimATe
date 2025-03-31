using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private InputManager inputManager;

    private float horizontal;
    private bool isFacingRight = true;
    private Transform originalParent;
    private Boolean isReceivingInput;

    [Header("Movement")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxAirSpeed;
    [SerializeField] private float bounceStrength;

    [SerializeField] private float jumpForce;
    [SerializeField] private float airMultiplier;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    // add the listeners for the events that InputManager throws and get the rigidbody
    void Start()
    {
        inputManager.playerOneOnMove.AddListener(MovePlayer);
        inputManager.playerOneOnJump.AddListener(Jump);
        inputManager.playerOneOnJumpEnd.AddListener(JumpEnd);
        rb = GetComponent<Rigidbody2D>();
        originalParent = transform.parent;
    }

    // update ensures that the player doesn't go above the speed limit and handles flipping the character for sprite stuff
    void Update()
    {        
        if (LevelManager.Instance != null && LevelManager.Instance.IsPaused())
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        SpeedControl();
        Flip();
        if(!IsGrounded() && rb.linearVelocity.y < 0.001)
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("isJumping", false);
        } else
        {
            animator.SetBool("isFalling", false);
        }

        if (rb.linearVelocity.y == 0)
            animator.SetBool("isJumping", false);
    }

    // adds force to the rigidbody to move the character. Applies a different speed if the character is in the air
    public void MovePlayer(Vector2 input)
    {
        //pause player if pause menu is activated
        if (LevelManager.Instance != null && LevelManager.Instance.IsPaused())
        {
            return;
        }

        if (IsGrounded())
        {
            rb.AddForce(input.normalized * playerSpeed, ForceMode2D.Force);
            //animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
            
            if (Mathf.Abs(input.normalized.x) == 1)
            {
                isReceivingInput = true;
            }
            else
            {
                isReceivingInput = false;
            }
            Debug.Log("is player recieving input: " + isReceivingInput);
            
            animator.SetFloat("Speed", Mathf.Abs(input.normalized.x));
        }


        else if (!IsGrounded())
            rb.AddForce(input.normalized * playerSpeed * airMultiplier, ForceMode2D.Force);

        horizontal = input.normalized.x;
    }

    // handles flipping the character
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // checks if the player is on the ground by using the OverlapCircle method. the groundLayer should be the ground tag in the game
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        
    }

    // handles jumps. makes the y component equal to the jump force
    public void Jump()
    {
        //pause player if pause menu is activated
        if (LevelManager.Instance != null && LevelManager.Instance.IsPaused())
        {
            return;
        }

        if (IsGrounded())
        {
            animator.SetBool("isJumping", true);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    // handles the end of the jump. This makes the player slow their upward movement and end the jump early if they tap vs hold the jump key
    public void JumpEnd()
    {
        //pause player if pause menu is activated
        if (LevelManager.Instance != null && LevelManager.Instance.IsPaused())
        {
            return;
        }

        if (rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    // enforces the speed limit. Its a school zone after all
    // has a different speed limit for when in the air vs on the ground, controlled by playerSpeed and maxAirSpeed
    public void SpeedControl()
    {
        Vector2 flatVelocity = new Vector2(rb.linearVelocity.x, 0f);

        
        if (flatVelocity.magnitude > maxSpeed)
        {
            Vector2 limitedVelocity = flatVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector2(limitedVelocity.x, rb.linearVelocity.y);
        }

        if (!IsGrounded() && flatVelocity.magnitude > maxAirSpeed)
        {
            Vector2 limitedVelocity = flatVelocity.normalized * maxAirSpeed;
            rb.linearVelocity = new Vector2(limitedVelocity.x, rb.linearVelocity.y);
        }
        if ((!isReceivingInput) && (rb.linearVelocity.y == 0))
        {
            Debug.Log("call quicklystop");
            quicklyStop();
        }
    }
    public void quicklyStop()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x*0.9f, rb.linearVelocity.y);
    }

    public void SetParent(Transform newParent)
    {
        originalParent = transform.parent;
        transform.parent = newParent;
        rb.interpolation = RigidbodyInterpolation2D.None;
    }

    public void ResetParent()
    {
        transform.parent = originalParent;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && transform.position.y > collision.transform.position.y + 0.2)
        {
            Debug.Log("Collision with Enemy");
            animator.SetBool("isJumping", true);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceStrength);
        }
    }
}