using UnityEngine;

public class ImageDestroyer : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("isCinematicRun", 0) == 1)
        {
            Destroy(gameObject);
        }
    }
}
