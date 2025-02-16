using UnityEngine;

public class ShopManagement : MonoBehaviour
{
    public static ShopManagement instance;
    [SerializeField] public CoinUIUpdater coinUIUpdater;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void PurchaseSkin(int skinIndex)
    {
        int skinPrice = 0;
        switch (skinIndex)
        {
            case 0:
                skinPrice = 0;
                break;
            case 1:
                skinPrice = 99;
                break;
            case 2:
                skinPrice = 499;
                break;
            case 3:
                skinPrice = 1199;
                break;
            case 4:
                skinPrice = 1999;
                break;
            default:
                Debug.Log("Invalid index of the ball skins.");
                break;
        }

        if (skinIndex < 0 || skinIndex >= Shop.instance.GetUnlockedSkins().Length) return;

        if (!Shop.instance.GetUnlockedSkins()[skinIndex] && AudioManager.coinCount >= skinPrice)
        {
            Debug.Log("Coins before purchase: " + AudioManager.coinCount);
            AudioManager.coinCount -= skinPrice;
            Shop.instance.UnlockSkin(skinIndex);
            coinUIUpdater.UpdateCoinUI();
            Debug.Log("Coins after purchase: " + AudioManager.coinCount);
            SaveProgress();
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    public void PurchaseTheme(int themeIndex)
    {
        int themePrice = 0;
        switch (themeIndex)
        {
            case 0:
                themePrice = 0;
                break;
            case 1:
                themePrice = 299;
                break;
            case 2:
                themeIndex = 799;
                break;
            case 3:
                themeIndex = 1799;
                break;
            case 4:
                themePrice = 2499;
                break;
            default:
                Debug.Log("Invalid index of the ball skins.");
                break;
        }

        if (themeIndex < 0 || themeIndex >= Shop.instance.GetUnlockedThemes().Length) return;

        if (!Shop.instance.GetUnlockedThemes()[themeIndex] && AudioManager.coinCount >= themePrice)
        {
            AudioManager.coinCount -= themePrice;
            Shop.instance.UnlockTheme(themeIndex);
            coinUIUpdater.UpdateCoinUI();
            SaveProgress();
        }

        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    public void SelectSkin(int skinIndex)
    {
        Debug.Log("Select Skin button pressed");
        if (Shop.instance.GetUnlockedSkins()[skinIndex])
        {
            Shop.instance.SetSkin((Shop.BallSkins)skinIndex);
            Debug.Log("Ball selected skin: " + skinIndex);
            SaveProgress();
        }
        else
        {
            Debug.Log("Ball not purchased yet!");
        }
    }

    public void SelectTheme(int themeIndex)
    {
        if (Shop.instance.GetUnlockedThemes()[themeIndex])
        {
            Shop.instance.SetTheme((Shop.Themes)themeIndex);
            SaveProgress();
        }
        else
        {
            Debug.Log("Theme not Purchased yet!");
        }
    }

    

    private void SaveProgress()
    {
        SaveSystem.SavePlayer(new Player());
    }
}
