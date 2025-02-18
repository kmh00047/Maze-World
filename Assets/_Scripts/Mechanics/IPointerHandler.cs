using UnityEngine;
using UnityEngine.EventSystems;

public class IPointerHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool moveLeft; // Set this in Inspector (true for Left, false for Right)
    private Movement playerMovement;

    private void Start()
    {
        playerMovement = FindAnyObjectByType<Movement>(); // Find Movement script
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (moveLeft)
            playerMovement.OnMoveLeft(true);
        else
            playerMovement.OnMoveRight(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (moveLeft)
            playerMovement.OnMoveLeft(false);
        else
            playerMovement.OnMoveRight(false);
    }
}
