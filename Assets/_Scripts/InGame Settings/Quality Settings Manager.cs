using UnityEngine;
using TMPro;

public class QualitySettingsManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown qualityDropdown; // Reference to the TMP_Dropdown

    private static bool isQualityInitialized = false; // Tracks if quality has been initialized

    void Start()
    {
        // Set the quality to Medium (index 2) only if it hasn't been initialized yet
        if (!isQualityInitialized)
        {
            QualitySettings.SetQualityLevel(2);
            isQualityInitialized = true;
        }

        // Initialize the dropdown value to match the current quality level
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();

        // Add listener to handle quality changes via dropdown
        qualityDropdown.onValueChanged.AddListener(SetQualityLevel);
    }

    private void SetQualityLevel(int index)
    {
        // Set Unity's quality level for the current session
        QualitySettings.SetQualityLevel(index);
    }

    private void OnDestroy()
    {
        // Remove listener to avoid memory leaks
        qualityDropdown.onValueChanged.RemoveListener(SetQualityLevel);
    }
}
