using System.Collections;
using UnityEngine;
using TMPro;

public class CameraCinematic : MonoBehaviour
{
    public float waitTimeAtEachStage = 5f;
    public float textSpeed = 0.05f;
    public TextMeshProUGUI textDisplay;

    private string[] tutorialTexts = new string[]
    {
        "Use the A and D button to MOVE \nUse the Space bar to jump",
        "Press and hold the Space bar to EXPAND the ball",
        "Use the MAP to reach the POT"
    };

    private void Start()
    {
        if (textDisplay == null)
        {
            Debug.LogError("TextDisplay is not assigned.");
            return;
        }
        if (PlayerPrefs.GetInt("isCinematicRun", 0) == 0)
        {

            StartCoroutine(RunTutorial());
        }
        else
        {
            Destroy(textDisplay.gameObject);
        }
    }

    private IEnumerator RunTutorial()
    {
        foreach (string text in tutorialTexts)
        {
            yield return StartCoroutine(TypeText(text));
            yield return new WaitForSecondsRealtime(waitTimeAtEachStage);
        }

        Destroy(textDisplay.gameObject);
        PlayerPrefs.SetInt("isCinematicRun", 1);
    }

    private IEnumerator TypeText(string fullText)
    {
        textDisplay.text = "";
        foreach (char letter in fullText)
        {
            textDisplay.text += letter;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }
}
