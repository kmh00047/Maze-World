using UnityEngine;
using UnityEngine.UI;

public class InterstitialButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowRewardedAd);
    }

    void ShowRewardedAd()
    {
        AdsManager.Instance.interstitialAds.ShowAd();
    }
}
