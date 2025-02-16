[System.Serializable]
public class PlayerData
{
    public int Level;
    public int Coins;
    public Shop.BallSkins SelectedSkin;
    public Shop.Themes SelectedTheme;

    public bool[] UnlockedSkins;
    public bool[] UnlockedThemes;
    public float[] HighScores;

    public PlayerData(Player player)
    {
        Level = player.Level;
        Coins = player.Coins;
        SelectedSkin = player.SelectedSkin;
        SelectedTheme = player.SelectedTheme;

        UnlockedSkins = player.UnlockedSkins;
        UnlockedThemes = player.UnlockedThemes;
        HighScores = player.HighScores;
    }
}
