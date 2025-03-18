using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [Header("Player One Keybinds")]
    private KeyCode playerOneLeft = KeyCode.A;
    private KeyCode playerOneRight = KeyCode.D;
    private KeyCode playerOneJump = KeyCode.A;

    [Header("Player Two Keybinds")]
    private KeyCode playerTwoLeft = KeyCode.LeftArrow;
    private KeyCode playerTwoRight = KeyCode.RightArrow;
    private KeyCode playerTwoJump = KeyCode.UpArrow;

    public UnityEvent<Vector2> PlayerOneOnMove = new UnityEvent<Vector2>();
    public UnityEvent PlayerOneOnJump = new UnityEvent();


    void Update()
    {
        Vector2 inputVector = new Vector2();

        if (Input.GetKeyDown(playerOneLeft))
            inputVector += Vector2.left;
        if (Input.GetKeyDown(playerOneRight))
            inputVector += Vector2.right;

        PlayerOneOnMove?.Invoke(inputVector);
        if (Input.GetKeyDown(playerOneJump))
            PlayerOneOnJump?.Invoke();
    }
}
