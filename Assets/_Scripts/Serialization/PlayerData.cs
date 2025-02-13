using UnityEngine;
using static Shop;

[System.Serializable]
public class PlayerData
{
    public int Level;
    public int Coins;
    public BallSkins SelectedSkin; // Store selected skin

    public PlayerData(Player player)
    {
        Level = player.Level;
        Coins = player.Coins;
        SelectedSkin = player.SelectedSkin; // Get selected skin from Player
    }
}
