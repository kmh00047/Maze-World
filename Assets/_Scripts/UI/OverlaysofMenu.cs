using UnityEngine;

public class MenuController : MonoBehaviour
{
    public enum MenuState { Main, Level, Options, Credits, Shop, Quit }

    [Header("Menu Screens")]
    public GameObject mainMenu;
    public GameObject levelMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;
    public GameObject shopMenu;
    public GameObject quitMenu;

    private void Start()
    {
        SetMenu(MenuState.Main);
    }

    public void SetMenu(MenuState state)
    {
        mainMenu.SetActive(state == MenuState.Main);
        levelMenu.SetActive(state == MenuState.Level);
        optionsMenu.SetActive(state == MenuState.Options);
        creditsMenu.SetActive(state == MenuState.Credits);
        shopMenu.SetActive(state == MenuState.Shop);
        quitMenu.SetActive(state == MenuState.Quit);

        Debug.Log($"Menu switched to: {state}");
    }

    // Button Methods
    public void StartGame() => SetMenu(MenuState.Level);
    public void OpenOptions() => SetMenu(MenuState.Options);
    public void OpenCredits() => SetMenu(MenuState.Credits);
    public void OpenShop() => SetMenu(MenuState.Shop);
    public void OpenQuitMenu() => SetMenu(MenuState.Quit);
    public void BackToMain() => SetMenu(MenuState.Main);

    public void QuitGame()
    {
        Debug.Log("Application Quitting...");
        Application.Quit();
    }
}
