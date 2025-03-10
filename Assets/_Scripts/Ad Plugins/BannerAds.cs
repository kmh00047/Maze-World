using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    [SerializeField] BannerPosition _bannerPosition;
    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null;


    void Awake()
    {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
        Advertisement.Banner.SetPosition(_bannerPosition); // Set the banner position
    }

    public void LoadBanner()
    {
        // Some bug, to be fixed in later updates
        return;

        AdsManager.Instance.UpdateDebug("Load Banner");
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
        AdsManager.Instance.UpdateDebug("Loaded CallBacks");
        Advertisement.Banner.Load(_androidAdUnitId, options);
    }

    void OnBannerLoaded() 
    { 
        Debug.Log("Banner loaded");
        AdsManager.Instance.UpdateDebug("Banner loaded");
        AdsManager.Instance.ShowBannerAd();
    }

    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        AdsManager.Instance.UpdateDebug("Error: " + message);
    }

    public void ShowBannerAd()
    {
        // Some bug, to be fixed in later updates
        return;

        AdsManager.Instance.UpdateDebug("Show Banner called");
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        Advertisement.Banner.Show(_adUnitId, options);
        AdsManager.Instance.UpdateDebug("Banner ad shown");
    }

    public void HideBannerAd() 
    {
        // Some bug, to be fixed in later updates
        return;


        Advertisement.Banner.Hide(); 
    }

    void OnBannerClicked() => Debug.Log("Banner clicked");
    void OnBannerShown() => Debug.Log("Banner shown");
    void OnBannerHidden() => Debug.Log("Banner hidden");
}
