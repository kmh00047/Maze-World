using UnityEngine;
using UnityEngine.InputSystem;

public class CursorHider : MonoBehaviour
{
    private bool isCursorHidden = false;

    void Update()
    {
        // Hide cursor when any key is pressed
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            HideCursor();
        }

        // Show cursor when the mouse moves
        if (Mouse.current.delta.ReadValue() != Vector2.zero)
        {
            ShowCursor();
        }
    }

    void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isCursorHidden = true;
    }

    void ShowCursor()
    {
        if (isCursorHidden)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            isCursorHidden = false;
        }
    }
}
