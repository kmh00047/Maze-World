using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PowerUpFloat : MonoBehaviour
{

    public GameObject gameOverScreen, gamePlayScreen;

    public float amplitude = 0.3f; 
    public float frequency = 2f;
    public GameObject gamePlayUI;
    
    public AudioManager AudioManagerInstance = AudioManager.instance;
    private Vector3 startPosition;

    void Start()
    {
        gameOverScreen.SetActive(false);
        startPosition = transform.position;
        AudioManagerInstance = FindAnyObjectByType<AudioManager>();
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
            if(AudioManagerInstance != null)
            {
                // Method being called in AudioManager
                AudioManagerInstance.InkLevelNumber(SceneManager.GetActiveScene().name);
                AudioManagerInstance.PlayLevelEndAudio();
            }
            gameOverScreen.SetActive(true);
            gamePlayScreen.SetActive(false);
            gamePlayUI.SetActive(false);
        }
    }
}
