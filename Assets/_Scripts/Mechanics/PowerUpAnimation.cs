using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PowerUpFloat : MonoBehaviour
{

    public GameObject gameOverScreen, gamePlayScreen;
    public Timer timer;
    public float amplitude = 0.3f; 
    public float frequency = 2f;
    public GameObject gamePlayUI;
    
    private Vector3 startPosition;
    private TextMeshProUGUI rewardText;
    void Start()
    {
        gameOverScreen.SetActive(false);
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Y position based on sine wave
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;

        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collided with the powerup
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has completed the level.");
            if(AudioManager.instance != null)
            {
                // Method being called in AudioManager
                AudioManager.instance.InkLevelNumber(SceneManager.GetActiveScene().name, timer.endTime);
                AudioManager.instance.PlayLevelEndAudio();
            }
            gameOverScreen.SetActive(true);
            gamePlayScreen.SetActive(false);
            gamePlayUI.SetActive(false);

        }
    }
}
