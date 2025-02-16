using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    public enum BallSkins { Ball1, Ball2, Ball3, Ball4, Ball5 }
    public enum Themes { Theme1, Theme2, Theme3, Theme4, Theme5 }

    [SerializeField] private List<GameObject> skins = new List<GameObject>();
    private Dictionary<BallSkins, GameObject> skinPrefabs;
    private BallSkins selectedSkin = BallSkins.Ball1; // Default

    [SerializeField] private List<ThemeData> themeDatas = new List<ThemeData>();
    private Dictionary<Themes, ThemeData> themes;
    private Themes selectedTheme = Themes.Theme1; // Default
    [SerializeField] private GameObject selectIconPrefab;
    [SerializeField] private RectTransform[] selectBallIconPosition = new RectTransform[5];
    [SerializeField] private RectTransform[] selectThemeIconPosition = new RectTransform[5];


    [SerializeField] public SpriteRenderer backgroundImage;
    [SerializeField] private Volume postProcessingVolume;

    private GameObject ballSelectIcon; // Reference to ball select icon
    private GameObject themeSelectIcon; // Reference to theme select icon 
    private bool[] unlockedSkins = new bool[5] { true, false, false, false, false };
    private bool[] unlockedThemes = new bool[5] { true, false, false, false, false };

    void Awake()
    {
        if (instance == null)
            instance = this;

        InitializeSkins();
        InitializeThemes();
        LoadProgress(); // Load saved progress
    }

    public void FindUIReferences()
    {
        // Find the Coin UI Updater again
        ShopManagement.instance.coinUIUpdater = GameObject.FindAnyObjectByType<CoinUIUpdater>();

        if (ShopManagement.instance.coinUIUpdater == null)
        {
            Debug.LogError("CoinUIUpdater not found! Make sure it's in the Menu scene.");
        }

        // Reassign select icon positions
        GameObject ballPositionsParent = GameObject.Find("SelectBallIconPositions");
        GameObject themePositionsParent = GameObject.Find("SelectThemeIconPositions");

        if (ballPositionsParent != null)
        {
            int childCount = ballPositionsParent.transform.childCount;
            selectBallIconPosition = new RectTransform[childCount];

            for (int i = 0; i < childCount; i++)
            {
                selectBallIconPosition[i] = ballPositionsParent.transform.GetChild(i).GetComponent<RectTransform>();
            }
        }
        else
        {
            Debug.LogError("SelectBallIconPositions not found! Make sure it exists in the Menu scene.");
        }

        if (themePositionsParent != null)
        {
            int childCount = themePositionsParent.transform.childCount;
            selectThemeIconPosition = new RectTransform[childCount];

            for (int i = 0; i < childCount; i++)
            {
                selectThemeIconPosition[i] = themePositionsParent.transform.GetChild(i).GetComponent<RectTransform>();
            }
        }
        else
        {
            Debug.LogError("SelectThemeIconPositions not found! Make sure it exists in the Menu scene.");
        }
    }


    void InitializeSkins()
    {
        skinPrefabs = new Dictionary<BallSkins, GameObject>
        {
            { BallSkins.Ball1, skins[0] },
            { BallSkins.Ball2, skins[1] },
            { BallSkins.Ball3, skins[2] },
            { BallSkins.Ball4, skins[3] },
            { BallSkins.Ball5, skins[4] }
        };
    }

    void InitializeThemes()
    {
        themes = new Dictionary<Themes, ThemeData>
        {
            { Themes.Theme1, themeDatas[0] },
            { Themes.Theme2, themeDatas[1] },
            { Themes.Theme3, themeDatas[2] },
            { Themes.Theme4, themeDatas[3] },
            { Themes.Theme5, themeDatas[4] }
        };
    }

    public void ApplySkin(GameObject player)
    {
        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }

        GameObject newSkin = Instantiate(skinPrefabs[selectedSkin], player.transform);
        newSkin.transform.localPosition = Vector3.zero;
    }

    public void SetSkin(BallSkins newSkin)
    {
        if (unlockedSkins[(int)newSkin])
        {
            selectedSkin = newSkin;
        }
        else
        {
            Debug.Log("Selected skin is not unlocked yet");
        }

        // Destroy old icon if exists
        if (ballSelectIcon != null)
        {
            Destroy(ballSelectIcon);
        }

        // Instantiate the new selection icon
        ballSelectIcon = Instantiate(selectIconPrefab, selectBallIconPosition[(int)newSkin]);

        // Set UI parent properly
        ballSelectIcon.transform.SetParent(selectBallIconPosition[(int)newSkin], false);

        // Offset the position (Bottom Right like a verification badge)
        RectTransform rectTransform = ballSelectIcon.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(40f, -40f); // Adjust values as needed
    }

    public void SetTheme(Themes newTheme)
    {
        if (unlockedThemes[(int)newTheme])
        {
            selectedTheme = newTheme;
        }

        ApplyTheme();

        // Destroy old icon if exists
        if (themeSelectIcon != null)
        {
            Destroy(themeSelectIcon);
        }

        // Instantiate the new selection icon
        themeSelectIcon = Instantiate(selectIconPrefab, selectThemeIconPosition[(int)newTheme]);

        // Set UI parent properly
        themeSelectIcon.transform.SetParent(selectThemeIconPosition[(int)newTheme], false);

        // Offset the position (Bottom Right like a verification badge)
        RectTransform rectTransform = themeSelectIcon.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(40f, -40f); // Adjust values as needed
    }



    public BallSkins GetSelectedSkin() => selectedSkin;

    public void ApplyTheme()
    {
        if (themes.ContainsKey(selectedTheme))
        {
            ThemeData theme = themes[selectedTheme];

            if (backgroundImage != null)
                backgroundImage.sprite = theme.backgroundImage;

            if (postProcessingVolume != null && theme.postProcessingProfile != null)
                postProcessingVolume.profile = theme.postProcessingProfile;
        }
        else
        {
            Debug.LogError("Selected theme not found!");
        }
    }


    public Themes GetSelectedTheme() => selectedTheme;

    public void UnlockSkin(int skinIndex) => unlockedSkins[skinIndex] = true;
    public bool[] GetUnlockedSkins() => unlockedSkins;

    public void UnlockTheme(int themeIndex) => unlockedThemes[themeIndex] = true;
    public bool[] GetUnlockedThemes() => unlockedThemes;




    public void LoadProgress()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            selectedSkin = data.SelectedSkin;
            selectedTheme = data.SelectedTheme;
            unlockedSkins = data.UnlockedSkins;
            unlockedThemes = data.UnlockedThemes;

            if(unlockedSkins == null)
            {
                Debug.Log("Unlocked Skins are null. Assigning default values");
                unlockedSkins = new bool[5] { true, false, false, false, false };
            }
            if(unlockedThemes == null)
            {
                Debug.Log("Unlocked themes are null. Assigning default themes");
                unlockedThemes = new bool[5] { true, false, false, false, false };
            }
        }
    }
}
