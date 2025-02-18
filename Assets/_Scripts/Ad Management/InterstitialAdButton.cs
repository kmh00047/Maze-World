using UnityEngine;
using UnityEngine.UI;

public class InterstitialAdButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowInterstitialAd);
    }

    private void ShowInterstitialAd()
    {
        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.interstitialAds.ShowInterstitialAd();
        }
        else
        {
            Debug.LogError("AdsManager Instance is null!");
        }
    }
}
