using UnityEditor;
using UnityEngine;

public class PlayerPrefsResetTool : EditorWindow
{
    [MenuItem("Tools/Set ForceReset to 1")]
    public static void SetForceReset()
    {
        PlayerPrefs.SetInt("ForceReset", 1);
        PlayerPrefs.Save();
        Debug.Log("ForceReset set to 1. The next game launch will clear PlayerPrefs.");
    }
}
