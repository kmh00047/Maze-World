using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class RewardedAdButton : MonoBehaviour
{
    [Tooltip("Check if it is Shop Ad")]
    [SerializeField] private bool isShopAd;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowRewardedAd);
    }

    private void ShowRewardedAd()
    {
        if (AdsManager.Instance != null)
        {
            if (isShopAd)
            {
                AdsManager.Instance.rewardedAds.ShowRewardedAd(RewardType.ShopCoins);
            }
            else
            {
                AdsManager.Instance.rewardedAds.ShowRewardedAd(RewardType.DoubleLevelReward);

            }
        }
        else
        {
            Debug.LogError("AdsManager Instance is null!");
        }
    }
}
