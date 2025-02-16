public class Player
{
    public int Level;
    public int Coins;
    public Shop.BallSkins SelectedSkin;
    public Shop.Themes SelectedTheme;

    public bool[] UnlockedSkins;
    public bool[] UnlockedThemes;
    public float[] HighScores;

    public Player()
    {
        Level = AudioManager.levelNumber;
        Coins = AudioManager.coinCount;

        SelectedSkin = Shop.instance.GetSelectedSkin();
        SelectedTheme = Shop.instance.GetSelectedTheme();

        UnlockedSkins = Shop.instance.GetUnlockedSkins();
        UnlockedThemes = Shop.instance.GetUnlockedThemes();
        HighScores = AudioManager.instance.HighScores;
    }
}
