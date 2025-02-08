using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lava : MonoBehaviour
{
    private AudioManager audioManager = AudioManager.instance;

    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Easiest script of the whole game (*_*)
        if(collision.gameObject.name == "Player")
        {
            Debug.Log("Player burnt in Lava");
            if(audioManager != null) 
            audioManager.PlayLavaBurnAudio();

            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
