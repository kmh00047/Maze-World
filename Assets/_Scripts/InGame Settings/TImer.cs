using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    private float startTime;
    public float endTime;
    private TextMeshProUGUI elapsedTimeText;

    void Start()
    {
        // Record the start time when the level begins
        startTime = Time.time;
        // Get the Text component attached to this GameObject
        elapsedTimeText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Calculate the elapsed time
        float elapsedTime = Time.time - startTime;

        // Format the elapsed time into minutes and seconds
        string minutes = Mathf.Floor(elapsedTime / 60).ToString("00");
        string seconds = (elapsedTime % 60).ToString("00");

        // Update the Text component with the elapsed time
        elapsedTimeText.text = string.Format("{0}:{1}", minutes, seconds);
        endTime = elapsedTime;
    }
}
