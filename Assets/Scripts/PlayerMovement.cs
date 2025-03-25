using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private InputManager inputManager;

    [Header("Movement")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float maxAirSpeed;
    

    Vector2 moveDir;
    

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
