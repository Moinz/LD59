using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private bool wasLeftButtonPressed;
    private Vector2 cursorPosition;
    
    public bool WasLeftButtonPressed => wasLeftButtonPressed;
    public Vector2 CursorPosition => cursorPosition;

    private void Update()
    {
        if (Mouse.current == null)
        {
            wasLeftButtonPressed = false;
            return;
        }

        wasLeftButtonPressed = Mouse.current.leftButton.wasPressedThisFrame;
        cursorPosition = Mouse.current.position.ReadValue();
    }
}