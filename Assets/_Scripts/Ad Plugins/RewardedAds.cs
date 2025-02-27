using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null;
    private AwardType award;
    public enum AwardType
    {
        DoubleLevelReward,
        ShopReward
    }

    void Awake()
    {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd(AwardType rewardType)
    {
        Advertisement.Show(_adUnitId, this);
        award = rewardType;
        LoadAd();
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");

            if (award == AwardType.DoubleLevelReward)
            {
                Debug.Log("Level reward Doubled.");
                AudioManager.instance.DoubleLevelReward();
                SceneManager.LoadScene("Menu");
            }
            else if (award == AwardType.ShopReward)
            {
                Debug.Log("Rewarded 200 coins to the Player.");
                AudioManager.instance.RewardCoins();
            }
        }
    }

    public void OnUnityAdsAdLoaded(string adUnitId) { Debug.Log("Ad Loaded: " + adUnitId); }
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message) { Debug.Log($"Error loading Ad Unit {adUnitId}: {error} - {message}"); }
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message) { Debug.Log($"Error showing Ad Unit {adUnitId}: {error} - {message}"); }
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
}
