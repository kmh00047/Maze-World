using UnityEngine;
using static Shop;

public class Player
{
    public int Level;
    public int Coins;
    public BallSkins SelectedSkin; // Store selected skin

    public Player()
    {
        Level = AudioManager.levelNumber;
        Coins = AudioManager.coinCount;
        SelectedSkin = Shop.instance.GetSelectedSkin(); // Get skin from shop
    }
}
