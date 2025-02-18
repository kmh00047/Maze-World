using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelMenuController : MonoBehaviour
{
    public enum GameState { Gameplay, Paused, GameOver }

    [Header("Game Screens")]
    public GameObject gamePlay;
    public GameObject gamePlayUI;
    public GameObject pauseMenu;

    private void Start()
    {
        SetGameState(GameState.Gameplay);
    }

    void Update()
    {
        // Trigger function when Escape key is pressed
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            OnEscapePressed();
        }
    }

    void OnEscapePressed()
    {
        Debug.Log("Escape key pressed!");
        if (gamePlay.activeSelf)
        {
            SetGameState(GameState.Paused);
        }
        else
        {
            SetGameState(GameState.Gameplay);
        }
    }

    public void SetGameState(GameState state)
    {
        gamePlay.SetActive(state == GameState.Gameplay);
        gamePlayUI.SetActive(state == GameState.Gameplay);
        pauseMenu.SetActive(state == GameState.Paused);

        Debug.Log($"Game state switched to: {state}");
    }

    

    // Button Methods
    public void ResumeGame() => SetGameState(GameState.Gameplay);
    public void PauseGame() => SetGameState(GameState.Paused);
    public void ShowGameOverScreen()
    {
        SetGameState(GameState.GameOver);
        AudioManager.instance.rewardText = GameObject.Find("RewardText").GetComponent<TextMeshProUGUI>();

        if(AudioManager.instance.rewardText == null) AudioManager.instance.rewardText = FindInactiveObject("RewardText").GetComponent<TextMeshProUGUI>();
    }

    private GameObject FindInactiveObject(string name)
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
        Debug.Log("Reward Text Not found.");
        return null; // Return null if not found
    }
}
