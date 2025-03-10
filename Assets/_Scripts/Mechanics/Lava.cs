using UnityEngine;
using UnityEngine.SceneManagement;

public class Lava : MonoBehaviour
{
    [SerializeField] private Vector3 playerPosition;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Easiest script of the whole game (*_*)
        if(collision.gameObject.name == "Player")
        {
            Debug.Log("Player burnt in Lava");
            if(AudioManager.instance != null) 
            AudioManager.instance.PlayLavaBurnAudio();

            collision.gameObject.transform.localPosition = playerPosition;
            Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            if(playerRB != null)playerRB.linearVelocity = Vector2.zero;
        }
    }
}
