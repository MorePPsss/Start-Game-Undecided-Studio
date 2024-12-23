using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCursorController : MonoBehaviour
{
    public Texture2D defaultCursor; 
    public Texture2D clickCursor;    
    
    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
        SetCursor(defaultCursor);
        playerInput.UI.Click.performed += ctx =>
        {
            Debug.Log("Pressed" + UnityEngine.InputSystem.Mouse.current.position.ReadValue());
            SetCursor(clickCursor);
        };
        playerInput.UI.Release.performed += ctx =>
        {
            Debug.Log("Release" + UnityEngine.InputSystem.Mouse.current.position.ReadValue());
            SetCursor(defaultCursor);
        };
    }

    private void SetCursor(Texture2D cursorTexture)
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}