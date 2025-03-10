using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour
{
    public AdsInitializer initializeAds;
    public BannerAds bannerAds;
    public InterstitialAds interstitialAds;
    public RewardedAds rewardedAds;

    public TextMeshProUGUI DebugAd;

    public static AdsManager Instance { get; private set; }
    private string menuSceneName = "Menu";


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        DebugAd = DebugAd.GetComponent<TextMeshProUGUI>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       if(scene.name == menuSceneName)
       {
            bannerAds.ShowBannerAd();
       }
        else
        {
            bannerAds.HideBannerAd();
        }

    }

    public void LoadAds()
    {
        interstitialAds.LoadAd();
        rewardedAds.LoadAd();
        UpdateDebug("Banner load called");
        bannerAds.LoadBanner();
    }

    public void ShowBannerAd()
    {
        StartCoroutine(ShowBannerWithDelay());
    }

    IEnumerator ShowBannerWithDelay()
    {
        yield return new WaitForSeconds(1f);
        bannerAds.ShowBannerAd();
    }

    public void UpdateDebug(string message)
    {
        if (DebugAd != null)
        {
            DebugAd.text = message;
        }
        else
        {
            Debug.Log("Add the fucking text object from the hirarchy");
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

}
