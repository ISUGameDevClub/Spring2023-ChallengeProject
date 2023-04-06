using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoSettings : MonoBehaviour
{
    public int targetFrameRate = 60;
    public bool vSyncEnabled = true;
    public int antiAliasingLevel = 2;
    public bool fullScreenMode = true;
    public bool unlimitedFPS = false;

    public TMP_Dropdown resolutionDropdown;
    public Toggle unlimitedFPSToggle;

    Resolution[] resolutions;

    void Start()
    {
        // Set the target frame rate
        if (!unlimitedFPS)
        {
            Application.targetFrameRate = targetFrameRate;
        }

        // Enable or disable VSync
        QualitySettings.vSyncCount = vSyncEnabled ? 1 : 0;

        // Set the anti-aliasing level
        QualitySettings.antiAliasing = antiAliasingLevel;

        // Set the full-screen mode
        Screen.fullScreen = fullScreenMode;

        // Get the available resolutions and populate the dropdown options
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> resolutionOptions = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            Resolution resolution = resolutions[i];
            string option = resolution.width + " x " + resolution.height;
            resolutionOptions.Add(option);
            if (resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Set the unlimited FPS toggle state
        unlimitedFPSToggle.isOn = unlimitedFPS;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetTargetFrameRate(int frameRate)
    {
        targetFrameRate = frameRate;
        if (!unlimitedFPS)
        {
            Application.targetFrameRate = targetFrameRate;
        }
    }

    public void SetVSyncEnabled(bool enabled)
    {
        vSyncEnabled = enabled;
        QualitySettings.vSyncCount = vSyncEnabled ? 1 : 0;
    }

    public void SetAntiAliasingLevel(int level)
    {
        antiAliasingLevel = level;
        QualitySettings.antiAliasing = antiAliasingLevel;
    }

    public void SetFullScreenMode(bool fullScreen)
    {
        fullScreenMode = fullScreen;
        Screen.fullScreen = fullScreenMode;
    }

    public void SetUnlimitedFPS(bool unlimited)
    {
        unlimitedFPS = unlimited;
        if (unlimitedFPS)
        {
            Application.targetFrameRate = -1;
        }
        else
        {
            Application.targetFrameRate = targetFrameRate;
        }
    }
}