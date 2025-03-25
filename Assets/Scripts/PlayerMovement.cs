using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private InputManager inputManager;

    private float horizontal;
    private bool isFacingRight = true;

    [Header("Movement")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float maxAirSpeed;

    [SerializeField] private float jumpForce;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float jumpCooldown;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    

    void Start()
    {
        inputManager.playerOneOnMove.AddListener(MovePlayer);
        inputManager.playerOneOnJump.AddListener(Jump);
        inputManager.playerOneOnJumpEnd.AddListener(JumpEnd);
        rb = GetComponent<Rigidbody2D>();
    }

    public void MovePlayer(Vector2 input)
    {
        if (IsGrounded())
            rb.AddForce(input.normalized * playerSpeed, ForceMode2D.Force);

        else if(!IsGrounded())
            rb.AddForce(input.normalized * playerSpeed * airMultiplier, ForceMode2D.Force);

        horizontal = input.normalized.x;
    }

    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 1f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    public void JumpEnd()
    {
        if(rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }
}
