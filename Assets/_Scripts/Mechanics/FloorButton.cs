using UnityEngine;

public class FloorButton : MonoBehaviour
{

    public Transform Player;
    public Animator animator;


    private void Start()
    {
        Player = GetComponent<Transform>();
        if( Player == null)
        {
            Debug.LogError("Missing Transform Player error!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Rigid body is contact with the button.");
            animator.Play("FloorButtonDown");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.Play("FloorButtonUp");
        }
    }

}
