using UnityEngine;

public class ShopManagement : MonoBehaviour
{
    public static ShopManagement instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void PurchaseSkin(int skinIndex , int skinPrice)
    {
        if (skinIndex < 0 || skinIndex >= Shop.instance.GetUnlockedSkins().Length) return;

        if (!Shop.instance.GetUnlockedSkins()[skinIndex] && AudioManager.coinCount >= skinPrice)
        {
            AudioManager.coinCount -= skinPrice;
            Shop.instance.UnlockSkin(skinIndex);
            SaveProgress();
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    public void PurchaseTheme(int themeIndex , int themePrice)
    {
        if (themeIndex < 0 || themeIndex >= Shop.instance.GetUnlockedThemes().Length) return;

        if (!Shop.instance.GetUnlockedThemes()[themeIndex] && AudioManager.coinCount >= themePrice)
        {
            AudioManager.coinCount -= 150;
            Shop.instance.UnlockTheme(themeIndex);
            SaveProgress();
        }

        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    public void SelectSkin(int skinIndex)
    {
        if (Shop.instance.GetUnlockedSkins()[skinIndex])
        {
            Shop.instance.SetSkin((Shop.BallSkins)skinIndex);
            SaveProgress();
        }
    }

    public void SelectTheme(int themeIndex)
    {
        if (Shop.instance.GetUnlockedThemes()[themeIndex])
        {
            Shop.instance.SetTheme((Shop.Themes)themeIndex);
            SaveProgress();
        }
    }

    private void SaveProgress()
    {
        SaveSystem.SavePlayer(new Player());
    }
}
