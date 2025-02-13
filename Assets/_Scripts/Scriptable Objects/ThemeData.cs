using UnityEngine;
using UnityEngine.Rendering; // For post-processing effects

[CreateAssetMenu(fileName = "NewTheme", menuName = "Game Theme")]
public class ThemeData : ScriptableObject
{
    public Sprite backgroundImage; // Background image
    public VolumeProfile postProcessingProfile; // Post-processing settings
}
