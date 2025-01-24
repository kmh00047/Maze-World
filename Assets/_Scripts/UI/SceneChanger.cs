using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public void loadMenu()
    {
        Debug.Log("Loading menu scene...");
        SceneManager.LoadScene("Menu");
    }

    public void loadLevel(string scene)
    {
        SceneManager.LoadScene(scene); 
    }

}
