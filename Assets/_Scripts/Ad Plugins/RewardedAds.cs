using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public enum RewardType
{
    DoubleLevelReward,
    ShopCoins
}

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidAdUnitId;
    [SerializeField] private string iosAdUnitId;

    private string adUnitId;
    private RewardType currentRewardType;
    private bool isAdLoaded = false;

    private void Awake()
    {
#if UNITY_IOS
        adUnitId = iosAdUnitId;
#elif UNITY_ANDROID
        adUnitId = androidAdUnitId;
#endif
    }

    public void LoadRewardedAd()
    {
        isAdLoaded = false; // Reset the flag
        Advertisement.Load(adUnitId, this);
        Debug.Log("Rewarded Ad load initiated");
    }

    // Call this method with a reward type parameter when a button is pressed
    public void ShowRewardedAd(RewardType rewardType)
    {
        
        if (isAdLoaded)
        {
            currentRewardType = rewardType;
            Advertisement.Show(adUnitId, this);
        }
        else
        {
            Debug.Log("Rewarded Ad is not ready yet!");
        }
    }

    #region Load Callbacks
    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId == adUnitId)
        {
            isAdLoaded = true;
            Debug.Log("Rewarded Ad Loaded and Ready!");
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Failed to load rewarded ad ({placementId}): {error} - {message}");
        isAdLoaded = false;
    }
    #endregion

    #region Show Callbacks
    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Rewarded Ad Show Started");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Rewarded Ad Clicked");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Rewarded Ad Show failed: {error} - {message}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == adUnitId && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("Rewarded Ad Fully Watched.");
            
            switch (currentRewardType)
            {
                case RewardType.DoubleLevelReward:
                    
                    AudioManager.instance.DoubleLevelReward();
                    break;
                case RewardType.ShopCoins:
                    
                    AudioManager.instance.RewardCoins();
                    break;
                default:
                    Debug.LogWarning("Reward type not recognized.");
                    break;
            }
        }
        else
        {
            Debug.Log("Rewarded Ad was not fully watched.");
        }
        // Reload the ad after it's shown
        LoadRewardedAd();
    }
    #endregion
}
