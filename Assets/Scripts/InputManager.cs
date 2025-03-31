using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [Header("Player One Keybinds")]
    [SerializeField] private KeyCode playerOneLeft = KeyCode.A;
    [SerializeField] private KeyCode playerOneRight = KeyCode.D;
    [SerializeField] private KeyCode playerOneJump = KeyCode.W;

    [Header("Player Two Keybinds")]
    [SerializeField] private KeyCode playerTwoLeft = KeyCode.LeftArrow;
    [SerializeField] private KeyCode playerTwoRight = KeyCode.RightArrow;
    [SerializeField] private KeyCode playerTwoJump = KeyCode.UpArrow;

    public UnityEvent<Vector2> playerOneOnMove = new UnityEvent<Vector2>();
    public UnityEvent playerOneOnJumpEnd = new UnityEvent();
    public UnityEvent playerOneOnJump = new UnityEvent();


    void Update()
    {
        //pause everything if the pause menu is active
        if (LevelManager.Instance != null && LevelManager.Instance.IsPaused())
        {
            return;
        }

        Vector2 inputVector = new Vector2();

        if (Input.GetKey(playerOneLeft))
        {
            inputVector += Vector2.left;
        }

        if (Input.GetKey(playerOneRight))
        {
            inputVector += Vector2.right;
        }

            playerOneOnMove?.Invoke(inputVector);
        

        // there are two different jump events to handle a player holding vs tapping the jump button. allows for variable jump heights
        if (Input.GetKeyDown(playerOneJump))
        {
            playerOneOnJump?.Invoke();
        }
        if (Input.GetKeyUp(playerOneJump))
        {
            playerOneOnJumpEnd?.Invoke();
        }

    }
}