using Unity.VisualScripting;
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
