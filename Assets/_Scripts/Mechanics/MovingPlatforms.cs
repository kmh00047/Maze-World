using UnityEngine;
using UnityEngine.UIElements;

public class MovingPlatforms : MonoBehaviour
{
    [Tooltip("Drag and drop the parent object of the Player")]
    public GameObject gamePlayMenu;
    [Tooltip("Drag and drop the PlayerPrefab here from the project assets")]
    public GameObject PlayerPrefab;

    [Tooltip("Tick for vertical movement, else horizontal")]
    public bool isMovingVertical = false;
    public float frequency = 1.0f;
    public float amplitude = 1.0f;

    private Vector3 startPosition;
    private GameObject newBall = null;
    private GameObject player = null;
    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Move the platform horizontally 
        if (!isMovingVertical)
        {
            float newX = startPosition.x + Mathf.Sin(Time.time * (frequency * 0.25f)) * amplitude;

            transform.position = new Vector3(newX, startPosition.y, startPosition.z);
        }

        // Move it vertically
        else if (isMovingVertical)
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * (frequency * 0.25f)) * amplitude;

            transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        }

        if ((player != null) && (newBall != null))
        {
            newBall.transform.position = player.transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            newBall = Instantiate(PlayerPrefab, player.transform.position, player.transform.rotation);

            player.transform.SetParent(transform);

            Debug.Log("Player landed on the platform.");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(newBall != null)
            {
                Destroy(newBall);
                Debug.Log("Destroyed the new ball");
            }
            collision.gameObject.transform.SetParent(gamePlayMenu.transform);
            Debug.Log("Player exited the platform.");
        }
    }


}
