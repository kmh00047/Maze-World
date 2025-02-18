using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class IPointerJumpHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Movement movementScript;

    private void Awake()
    {
        // Automatically find the Movement script in the scene
        movementScript = FindAnyObjectByType<Movement>();

        if (movementScript == null)
        {
            Debug.LogError("Movement script not found! Make sure it's in the scene.");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (movementScript != null)
        {
            movementScript.SendMessage("OnJump", new InputAction.CallbackContext(), SendMessageOptions.DontRequireReceiver);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (movementScript != null)
        {
            movementScript.SendMessage("OnJumpRelease", new InputAction.CallbackContext(), SendMessageOptions.DontRequireReceiver);
        }
    }
}
