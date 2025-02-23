using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using TMPro;



public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    public AudioSource audioSource;
    [SerializeField] public TextMeshProUGUI rewardText;

    // Audio Clips to be shuffled through script
    public AudioClip buttonClick;
    public AudioClip levelEnd;
    public AudioClip lavaBurn;
    public AudioClip jump;
    public AudioClip rampJump;

    [Tooltip("Shows how many levels are completed")]
    public static int levelNumber = 0;
    public float[] HighScores = new float[10];

    [Tooltip("Buttons for all the levels in the Game")]
    public Button[] levelButtons;

    public string menuSceneName = "Menu"; 

    public static int coinCount = 0;

    private float levelReward;

    private void Awake()
    {
        //We check if the instance is null i.e we freshly started the game
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // GameObject persist when scene changes
            SceneManager.sceneLoaded += OnSceneLoaded; // Register an event named OnSceneLoaded
            // in SceneManager.sceneLoaded library
        }
        else
        {
            // If the gameObject has persisted from scene to scene, i.e instance already exist
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        //QualitySettings.SetQualityLevel(5);

        // Reset Player Prefs for debugging
        //PlayerPrefs.DeleteAll();
        //Debug.Log("Player Prefs Reset Successful");

    }
   
    // Def for the loaded event 
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == menuSceneName)
        {
            Shop.instance.FindUIReferences();
            LoadData();
            ManageButtons();
        }
        // For debugging purpose only
        //coinCount = 10000;
        
        Shop.instance.backgroundImage = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        if (Shop.instance.backgroundImage != null) Shop.instance.ApplyTheme();
        rewardText = GameObject.Find("RewardText").GetComponent<TextMeshProUGUI>();

    }

    private void AssignLevelButtons()
    { // Find all buttons in the scene, including inactive ones
      Button[] allButtons = Resources.FindObjectsOfTypeAll<Button>(); 
        
        // Reset the levelButtons array i.e to null (To specify the length)
        levelButtons = new Button[10]; 
        
        // Assign buttons based on their names
        
        for (int i = 0; i < levelButtons.Length; i++) 
        { 
            string buttonName = "Level " + (i + 1); 
            foreach (Button button in allButtons) 
            { 
                if (button.name == buttonName) 
                { 
                    levelButtons[i] = button; break; 
                } 
            } 
            
            if (levelButtons[i] == null) 
            { 
                Debug.LogError("Button " + buttonName + " not found."); 
            } 
        } 
    }
    private void ManageButtons()
    {
        /// Not sure why but works with UnityEngine.Object only
        Button[] buttons = UnityEngine.Object.FindObjectsByType<Button>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(PlayButtonClickAudio);
        }

        if (SceneManager.GetActiveScene().name == menuSceneName)
        {
            AssignLevelButtons();
            UpdateLevelButtons();
        }
    }
    private void UpdateLevelButtons()
    {
        // Disable all level buttons
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (levelButtons[i] != null)
            {
                levelButtons[i].interactable = false;
            }
            else
            {
                Debug.LogError("Button #" + (i + 1) + " not found.");
            }
        }

        // Enable buttons only for the unlocked levels
        for (int i = 0; i < levelNumber + 1; i++)
        {
            if (levelButtons[i] != null)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                Debug.LogError("Button #" + (i + 1) + " not found.");
            }
        }
    }
    // Called from the powerupAnimation script when player completes the level
    public void InkLevelNumber(string scene , float elapsedTime)
    {
        int levelIndex = SceneManager.GetSceneByName(scene).buildIndex;
        levelReward = 20f;
        levelReward = (levelReward + (10 * levelIndex)) * (1 + ((10 - elapsedTime) / 510));
        levelReward -= 20;
        if (levelReward < 5) levelReward = 5;

        float previousRecord = HighScores[levelIndex - 1];
        float minImprovement = previousRecord * 0.9f;  // Require at least 10% improvement
        

        // If it's a new high score and the improvement is significant
        if (elapsedTime < previousRecord || previousRecord == 0)
        {
            if (elapsedTime < minImprovement)  // Only grant bonus if it's 10% faster
            {
                levelReward *= 1.3f;
                Debug.Log("Significant speedrun improvement! Bonus applied.");
            }

            HighScores[levelIndex - 1] = elapsedTime;
            Debug.Log("New speedrun: Level " + levelIndex + " new speedrun time is " + elapsedTime);
        }
        else
        {
            Debug.Log("You completed the level in " + (int)elapsedTime + " seconds");
        }

        Debug.Log("Reward = " + (int)levelReward);
        coinCount += (int)levelReward;

        if (rewardText != null)
        {
            rewardText.text = $"Time Elapsed: {(int)elapsedTime} seconds\nPrvious Record = {(int)previousRecord}\nNitcoins Earned = {(int)levelReward}\nTotal Nitcoins = {coinCount}";
        }

        Debug.Log("Coins = " + coinCount);
        

        // Update level number to keep track of the number if levels completed
        if (levelNumber < SceneManager.GetSceneByName(scene).buildIndex)
        {
            levelNumber = SceneManager.GetSceneByName(scene).buildIndex;
        }

        if (SceneManager.GetActiveScene().name == menuSceneName)
        {
            UpdateLevelButtons();
        }
        SaveGame();
    }

    

    // Play different audios
    void PlayButtonClickAudio()
    {
        if (audioSource != null && buttonClick != null)
        {
            audioSource.clip = buttonClick;
            audioSource.PlayOneShot(buttonClick);}
    }

    public void PlayLevelEndAudio()
    {
        if (audioSource != null && levelEnd != null)
        {
            audioSource.clip = levelEnd;
            audioSource.PlayOneShot(levelEnd);        }
    }

    public void PlayLavaBurnAudio()
    {
        if (audioSource != null && lavaBurn != null)
        {
            audioSource.clip = lavaBurn;
            audioSource.PlayOneShot(lavaBurn);        }
    }

    public void PlayJumpAudio()
    {
        audioSource.clip = jump;
        audioSource.PlayOneShot(jump);    }

    public void PlayRampJumpAudio()
    {
        audioSource.clip = rampJump;
        audioSource.PlayOneShot(rampJump);    
    }
    // Destroy the registered event in sceneLoaded
    private void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    // <summary>
    // Save and Load System with Debugging
    // </summary>
    //void OnGUI()
    //{
    //    // Calculate FPS
    //    float fps = 1.0f / Time.deltaTime;
    //    GUI.Label(new Rect(10, 10, 200, 20), "FPS: " + fps.ToString("F2"));
    //}

    public void SaveGame()
    {
        Player player = new Player();

        if (player == null)
        {
            Debug.LogError("Player instance not created properly!");
            return;
        }

        SaveSystem.SavePlayer(player);
        Debug.Log("Game Saved Successfully.");
    }


    private void LoadData()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null) 
        {
            Debug.LogWarning("Data not loaded Properly!");
            ResetCinematic();
            return; 
        }

        levelNumber = data.Level;
        coinCount = data.Coins;
        HighScores = data.HighScores;
        Debug.Log("Data loaded successfully");
    }

    public void ChangeColorFromHex(SpriteRenderer spriteRenderer, string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color newColor))
        {
            spriteRenderer.color = newColor;
        }
        else
        {
            Debug.LogError("Invalid Hex Code!");
        }
    }

    public void ChangeColorFromRGB(SpriteRenderer spriteRenderer, float r, float g, float b)
    {
        spriteRenderer.color = new Color(r, g, b);
    }

    // Method to reset the cinematic and run it again
    public void ResetCinematic()
    {
        PlayerPrefs.SetInt("isCinematicRun", 0); // Reset the key to allow cinematic to run again
    }

    public void DoubleLevelReward()
    {
        coinCount += (int)levelReward;
    }

    public void RewardCoins()
    {
        coinCount += 200;
        ShopManagement.instance.coinUIUpdater.UpdateCoinUI();
    }

    public void AddCoins(int amount)
    {
        coinCount += amount;
        ShopManagement.instance.coinUIUpdater.UpdateCoinUI();
        SaveGame();
    }
}
