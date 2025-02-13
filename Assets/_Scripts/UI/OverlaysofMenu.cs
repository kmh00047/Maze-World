using UnityEngine;
using UnityEngine.SceneManagement;

public class OverlaysofMenu : MonoBehaviour
{
    /// <summary>
    /// Controlling different menus by booleans
    /// </summary>
    /// Could do it with Enumerators but I am lazy (hehe :)
    public GameObject quitMenu, optionsMenu, mainMenu, levelMenu, creditsMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        quitMenu.SetActive(false);
        optionsMenu.SetActive(false);
        levelMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }
    public void startButtonPressed()
    {
        Debug.Log("Going to Level Menu.");
        levelMenu.SetActive(true);
        mainMenu.SetActive(false);
        quitMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);

        // For debugging only
        AudioManager.coinCount++;
    }

    public void optionsButtonPressed()
    {
        Debug.Log("Options Button Pressed...");
        optionsMenu.SetActive(true);
        quitMenu.SetActive(false);
        mainMenu.SetActive(false);
        levelMenu.SetActive(false);
        creditsMenu.SetActive(false);

        // For debugging only
        AudioManager.coinCount--;
    }

    public void creditsButtonPressed()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true) ;
        optionsMenu.SetActive(false);
        levelMenu.SetActive(false);
        quitMenu .SetActive(false);
    }

    public void quitButtonPressed()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        quitMenu.SetActive(true);
        creditsMenu .SetActive(false) ;
        levelMenu .SetActive(false);
    }

    public void BackButtonPressed()
    {
        Debug.Log("Going back to main menu...");
        optionsMenu.SetActive(false);
        quitMenu.SetActive(false);
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void quitGame()
    {
        Debug.Log("Application Quiting...");
        Application.Quit();
    }
}
