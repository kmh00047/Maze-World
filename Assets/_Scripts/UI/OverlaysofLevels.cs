using UnityEngine;


public class OverlaysofLevels : MonoBehaviour
{

    public GameObject gamePlay, gamePlayUI, pauseMenu;

    private void Start()
    {
        gamePlay.SetActive(true);
        gamePlayUI.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void OnBackButtonPressed()
    {
            bool isGameplayActive = gamePlay.activeSelf;
            gamePlay.SetActive(!isGameplayActive);
            gamePlayUI.SetActive(!isGameplayActive);
            pauseMenu.SetActive(isGameplayActive);
    }

    public void goToGamePlay()
    {
       gamePlay.SetActive (true);
        gamePlayUI.SetActive(true);
        pauseMenu.SetActive (false);
    }

    public void goToGameOver()
    {
        pauseMenu.SetActive(true) ;
        gamePlayUI.SetActive (false) ;
        gamePlay.SetActive(false) ;
    }
}

    