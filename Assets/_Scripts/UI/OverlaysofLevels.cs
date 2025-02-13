using UnityEngine;

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
    public void ShowGameOverScreen() => SetGameState(GameState.GameOver);
}
