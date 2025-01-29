using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public TMP_Dropdown resolutionDropDown;

    public AudioMixer audioMixer; 
    public Slider masterVolumeSlider; 
    public Slider musicVolumeSlider; 

    Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions; // Get the available resolutions on system
        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolution = 0;
        for (int i = 0; i < resolutions.Length; i++)
        { 
            // Append all the resolutions in the resolutions array
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Getting the runtime resolution of the game
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }

        // Adding resolutions to UI dropDown
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolution;
        resolutionDropDown.RefreshShownValue();


        // Initialize sliders and toggle with current settings
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f); 
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f); 
        //fullscreenToggle.isOn = Screen.fullScreen; 
        
        // Apply saved settings (from sliders and toggle)
        SetMasterVolume(masterVolumeSlider.value); 
        SetMusicVolume(musicVolumeSlider.value); 
        //SetFullscreen(fullscreenToggle.isOn);
    }

    // Def for the setResolution method
    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    public void SetMasterVolume(float volume)
    {
        // Using logarithmic scale because some guy on discord told me to do so :)
        float decibels = Mathf.Log10(volume) * 20; 
        if (volume == 0)
        {
            decibels = -80; 
            // Mute the volume if slider is at the extreme left
        } 
        audioMixer.SetFloat("MasterVolume", decibels); 
        PlayerPrefs.SetFloat("MasterVolume", volume); 
    } 
    
    public void SetMusicVolume(float volume) 
    { float decibels = Mathf.Log10(volume) * 20; 
        if (volume == 0) 
        { 
            decibels = -80; 
        } 
        
        audioMixer.SetFloat("MusicVolume", decibels); 
        PlayerPrefs.SetFloat("MusicVolume", volume); 
    }

    public void SetFullscreen(bool isFullscreen) 
    { 
        Screen.fullScreen = isFullscreen; 
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0); 
    }
}
