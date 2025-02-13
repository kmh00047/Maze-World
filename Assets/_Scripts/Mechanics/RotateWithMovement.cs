using UnityEngine;

public class BallRotation : MonoBehaviour
{
    public float rotationSpeed = 300f;  // Speed of rotation (adjustable)
    private Vector2 lastParentPosition;

    void Start()
    {
        if (transform.parent != null)
            lastParentPosition = transform.parent.position;
    }

    void Update()
    {
        if (transform.parent != null)
        {
            Vector2 currentParentPosition = transform.parent.position;
            Vector2 movementDirection = currentParentPosition - lastParentPosition;

            if (movementDirection.sqrMagnitude > 0.0001f)  // If the ball is moving
            {
                float distanceMoved = movementDirection.x;  // How much it moved
                if (distanceMoved < 0) distanceMoved *= -1f;
                float rotationAmount = distanceMoved * rotationSpeed;  // Convert movement to rotation
                if (Vector2.Dot(movementDirection, Vector2.right) > 0)
                {
                    transform.Rotate(0, 0, -rotationAmount);
                }// Rotate ball (negative for correct direction)
                else
                {
                    transform.Rotate(0,0,rotationAmount);
                }
            }

            lastParentPosition = currentParentPosition;  // Update last position
        }
    }
}
