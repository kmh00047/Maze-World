using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraCinematic : MonoBehaviour
{
    public Vector3 playerPosition;
    public Vector3 zoomOutPosition;
    public Vector3 gameEndPosition;

    public float zoomOutFOV = 60f;
    public float zoomInFOV = 30f;

    public float waitTimeAtEachStage = 1f;
    public float textSpeed = 0.05f;

    public Image canvasImage;
    public TextMeshProUGUI textDisplay;

    private string textAtPlayerPosition = "Welcome to the Maze World! \n Isn't it amazing. Oh wait \n You can't see it";
    private string textAtZoomOutPosition = "Sorry! Here it is! \n So you have only one goal in this game.";
    private string textAtGameEndPosition = "And that is to collect the Pots of Wisdom\n You need 10 of them to win";
    private string textAtZoomBackPosition = "The faster you get to it, the more you will be rewarded! \n Use the buttons on screen to Move.";
    private string textAtFinalPosition = "Touch anywhere on the screen to jump.\nGood luck and....... DON'T DIE, Haha!";

    private Camera cam;
    private float originalFOV;
    private Movement playerMovement;
    [SerializeField] GameObject Border;
    [SerializeField] GameObject Timer;
    [SerializeField] GameObject BackButton;

    void Start()
    {
        cam = Camera.main;
        originalFOV = cam.fieldOfView;

        playerMovement = null;
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerMovement = playerObject.GetComponent<Movement>();
        }

        
        if (PlayerPrefs.GetInt("isCinematicRun", 0) == 0)
        {
            
            StartCoroutine(CinematicSequence());
        }
        else
        {
            // If it's not the first time, just enable player movement
            if (playerMovement != null) playerMovement.enabled = true;
        }
    }

    IEnumerator CinematicSequence()
    {
        Time.timeScale = 0f; // Freeze time
        if (playerMovement != null) playerMovement.enabled = false;
        if (Border != null) Border.SetActive(false);
        if (Timer != null) Timer.SetActive(false);
        if (BackButton != null) BackButton.SetActive(false);

        
        yield return StartCoroutine(MoveCamera(playerPosition, zoomOutFOV, textAtPlayerPosition, waitTimeAtEachStage));
        yield return StartCoroutine(MoveCamera(zoomOutPosition, zoomOutFOV, textAtZoomOutPosition, waitTimeAtEachStage));
        yield return StartCoroutine(MoveCamera(gameEndPosition, zoomInFOV, textAtGameEndPosition, waitTimeAtEachStage));
        yield return StartCoroutine(MoveCamera(zoomOutPosition, zoomOutFOV, textAtZoomBackPosition, waitTimeAtEachStage));
        yield return StartCoroutine(MoveCamera(playerPosition, zoomInFOV, textAtFinalPosition, waitTimeAtEachStage));

        Time.timeScale = 1f; 
        if (playerMovement != null) playerMovement.enabled = true;
        if (Border != null) Border.SetActive(true);
        if (Timer != null) Timer.SetActive(true);
        if (BackButton != null) BackButton.SetActive(true);
        // Set PlayerPrefs to make the cinematic as run
        PlayerPrefs.SetInt("isCinematicRun", 1);

        Destroy(canvasImage.gameObject);
        Destroy(textDisplay.gameObject);
    }

    IEnumerator MoveCamera(Vector3 targetPosition, float targetFOV, string textToDisplay, float stayTime)
    {
        yield return StartCoroutine(TypeText(textToDisplay));

        float duration = 1f;
        float elapsed = 0f;
        Vector3 startPos = cam.transform.position;
        float startFOV = cam.fieldOfView;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;
            cam.transform.position = Vector3.Lerp(startPos, targetPosition, t);
            cam.fieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null;
        }

        cam.transform.position = targetPosition;
        cam.fieldOfView = targetFOV;
        yield return new WaitForSecondsRealtime(stayTime);
    }

    IEnumerator TypeText(string fullText)
    {
        textDisplay.text = "";
        foreach (char letter in fullText)
        {
            textDisplay.text += letter;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    
}
