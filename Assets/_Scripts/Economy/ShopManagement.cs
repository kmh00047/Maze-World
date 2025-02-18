using UnityEngine;

public class ShopManagement : MonoBehaviour
{
    public static ShopManagement instance;
    [SerializeField] public CoinUIUpdater coinUIUpdater;
    [SerializeField] private TimedMessage timedMessage;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void FindTimedMessage()
    {
        if (timedMessage == null)
        {
            timedMessage = GameObject.Find("TimedMessage").GetComponent<TimedMessage>();
        }
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

            string Output = "Ball Skin Purchased!";
            timedMessage = timedMessage.GetComponent<TimedMessage>();
            timedMessage.ShowMessage(Output);
            Debug.Log(Output);

            SaveProgress();
        }
        else
        {
            string Output = "Not Enough NitCoins";
            timedMessage = timedMessage.GetComponent<TimedMessage>();
            timedMessage.ShowMessage(Output);
            Debug.Log(Output);
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
                themePrice = 799;
                break;
            case 3:
                themePrice = 1799;
                break;
            case 4:
                themePrice = 2499;
                break;
            default:
                Debug.Log("Invalid index of the ball skins.");
                break;
        }
        Debug.Log("Decided Theme Price = " + themePrice);
        if (themeIndex < 0 || themeIndex >= Shop.instance.GetUnlockedThemes().Length) return;

        if (!Shop.instance.GetUnlockedThemes()[themeIndex] && AudioManager.coinCount >= themePrice)
        {
            Debug.Log("Coins before purchase: " + AudioManager.coinCount);
            AudioManager.coinCount -= themePrice;
            Shop.instance.UnlockTheme(themeIndex);
            coinUIUpdater.UpdateCoinUI();
            Debug.Log("Coins after purchase: " + AudioManager.coinCount);

            string Output = "Theme Purchased!";
            timedMessage = timedMessage.GetComponent<TimedMessage>();
            timedMessage.ShowMessage(Output);
            Debug.Log(Output);

            SaveProgress();
        }

        else
        {
            string Output = "Not Enough NitCoins";
            timedMessage = timedMessage.GetComponent<TimedMessage>();
            timedMessage.ShowMessage(Output);
            Debug.Log(Output);
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
            string Output = "Skin is not unlocked yet";
            timedMessage = timedMessage.GetComponent<TimedMessage>();
            timedMessage.ShowMessage(Output);
            Debug.Log(Output);
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
            string Output = "Theme is not unlocked yet";
            timedMessage = timedMessage.GetComponent<TimedMessage>();
            timedMessage.ShowMessage(Output);
            Debug.Log(Output);
        }
    }

    

    private void SaveProgress()
    {
        SaveSystem.SavePlayer(new Player());
    }
}
