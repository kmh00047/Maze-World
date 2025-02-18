using System.Collections;
using TMPro;
using UnityEngine;

public class TimedMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private float displayDuration = 2f;

    private Coroutine messageCoroutine;

    private void Start()
    {
        messageText = messageText.GetComponent<TextMeshProUGUI>();
    }
    public void ShowMessage(string message)
    {
        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }
        messageCoroutine = StartCoroutine(DisplayMessageRoutine(message));
    }

    private IEnumerator DisplayMessageRoutine(string message)
    {
        messageText.text = message;
        yield return new WaitForSeconds(displayDuration);
        messageText.text = "";
    }
}
