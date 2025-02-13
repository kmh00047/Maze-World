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

    [SerializeField] private SpriteRenderer backgroundImage;
    [SerializeField] private Volume postProcessingVolume;

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

    private void Start()
    {
         backgroundImage = GameObject.Find("Background").GetComponent<SpriteRenderer>();
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
        if (unlockedSkins[(int)newSkin]) selectedSkin = newSkin;
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

    public void SetTheme(Themes newTheme)
    {
        if (unlockedThemes[(int)newTheme]) selectedTheme = newTheme;
        ApplyTheme();
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
        }
    }
}
