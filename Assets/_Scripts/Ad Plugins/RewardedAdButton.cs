using UnityEngine;
using UnityEngine.UI;
using static RewardedAds;

public class RewardedAdButton : MonoBehaviour
{
    private Button button;
    [SerializeField] AwardType RewardType;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowRewardedAd);
    }

    void ShowRewardedAd()
    {
        AdsManager.Instance.rewardedAds.ShowAd(RewardType);
    }
}
