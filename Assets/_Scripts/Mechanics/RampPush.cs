using UnityEngine;

public class RampPush : MonoBehaviour
{
    
    public float force = 10f;
    public float waitingTime = 0.5f;
    public float Height = 0.3f;

    public Transform Ramp_Capsule;
    public Transform Ramp_Spring;
    public Transform Player;
    public Rigidbody2D playerRb;
    public Vector3 newScale = Vector3.one;

    private Vector3 currentScale;
    private Vector3 currentPosition;
    private Vector2 newPosition;
    private float waitTillRecoverScaleTime = 0;
    private AudioManager audioManager = AudioManager.instance;

    private void Start()
    {  
        audioManager = FindAnyObjectByType<AudioManager>();
            if (playerRb == null)
            {
                Debug.Log("Rigidbody2D component missing from the Player GameObject!");
                playerRb = Player.GetComponent<Rigidbody2D>();
            }
            currentScale = Ramp_Spring.transform.localScale;
            currentPosition = Ramp_Capsule.transform.localPosition;

        newPosition = new Vector2(Ramp_Capsule.transform.localPosition.x, Ramp_Capsule.transform.localPosition.y + Height);
    }

    private void Update()
    {
        // Wait till the spring contracts again
        if (waitTillRecoverScaleTime > 0)
        {
            waitTillRecoverScaleTime -= Time.deltaTime;
            Ramp_Spring.transform.localScale = newScale;
            Ramp_Capsule.transform.localPosition = newPosition;
        }

        if (waitTillRecoverScaleTime <= 0)
        {
            waitTillRecoverScaleTime = 0;
            Ramp_Spring.transform.localScale = currentScale;
            Ramp_Capsule.transform.localPosition = currentPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform == Player)
        {
            Debug.Log("Player collided with the Ramp.");
            if (collision.gameObject.transform.localScale.x <= 0.38f)
            {
                // Apply force to the player to make it bounce
                Vector2 bounceForce = new Vector2(0, force);

                // Using Impulse to instantly add all the force
                playerRb.AddForce(bounceForce, ForceMode2D.Impulse);
                if (audioManager != null)
                {
                    audioManager.PlayRampJumpAudio();
                }
                Debug.Log("Player velocity after force = " + playerRb.linearVelocity);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.transform == Player)
        {
            waitTillRecoverScaleTime = waitingTime;
        }
    }
}
