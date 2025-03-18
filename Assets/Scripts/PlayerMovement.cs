using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    [SerializeField] private int playerSpeed;

    Vector2 moveDir;
    Rigidbody2D rb;

    void Start()
    {
        inputManager.playerOneOnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePlayer(Vector2 input)
    {
        rb.AddForce(input.normalized * playerSpeed, ForceMode2D.Force);
    }
}
