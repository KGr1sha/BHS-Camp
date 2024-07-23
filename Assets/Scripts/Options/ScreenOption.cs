using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenOption : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown screenModeDropdown;

    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private int MaxRefreshRate { get; set; }

    private readonly List<Resolution> resolutions = new();

    List<string> screenModeOptions = new() {
            "Окно",
            "Полный экран",
        };

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        InitializeResolutions();
        InitializeScreenModes();
        LoadSavedOptions();
        UpdateDropdownValues();
    }

    private void UpdateDropdownValues()
    {
        resolutionDropdown.value = GetResolutionIndex();
        resolutionDropdown.RefreshShownValue();

        screenModeDropdown.value = GetScreenModeIndex();
        screenModeDropdown.RefreshShownValue();
    }

    private int GetResolutionIndex()
    {
        string resolutionWidth = "resolutionWidth", resolutionHeight = "resolutionHeight";
        int currentIndex = 0;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (resolutions[i].width == PlayerPrefs.GetInt(resolutionWidth) &&
                resolutions[i].height == PlayerPrefs.GetInt(resolutionHeight))
            {
                currentIndex = i;
                break;
            }
        }
        return currentIndex;
    }

    private int GetScreenModeIndex()
    {
        string screenMode = "screenMode";
        int screenModeInt = PlayerPrefs.GetInt(screenMode, (int)Screen.fullScreenMode);
        return screenModeInt;
    }

    private void InitializeResolutions()
    {
        var allResolutions = Screen.resolutions;
        MaxRefreshRate = allResolutions[^1].refreshRate;
        var uniqueResolutions = new List<string>();

        foreach (var resolution in allResolutions)
        {
            if (resolution.refreshRate == MaxRefreshRate)
            {
                uniqueResolutions.Add($"{resolution.width}x{resolution.height}");
                resolutions.Add(resolution);
            }
        }

        string resolutionWidth = "resolutionWidth", resolutionHeight = "resolutionHeight";
        int currentIndex = 0;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (resolutions[i].width == PlayerPrefs.GetInt(resolutionWidth) &&
                resolutions[i].height == PlayerPrefs.GetInt(resolutionHeight))
            {
                currentIndex = i;
                break;
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(uniqueResolutions);
        resolutionDropdown.value = currentIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void InitializeScreenModes()
    {
        screenModeDropdown.ClearOptions();
        List<string> screenModeDropdownOptions = screenModeOptions;
        screenModeDropdown.AddOptions(screenModeDropdownOptions);
        screenModeDropdown.RefreshShownValue();
    }

    private void LoadSavedOptions()
    {
        LoadScreenMode();
        LoadResolution();
    }

    private void LoadScreenMode()
    {
        string screenMode = "screenMode";
        int screenModeInt = PlayerPrefs.GetInt(screenMode, (int) FullScreenMode.FullScreenWindow);
        screenModeDropdown.value = screenModeInt;
        screenModeDropdown.RefreshShownValue();

        if (screenModeInt == (int)FullScreenMode.FullScreenWindow)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    private void LoadResolution()
    {
        string resolutionWidth = "resolutionWidth", resolutionHeight = "resolutionHeight";
        int width = PlayerPrefs.GetInt(resolutionWidth, resolutions[^1].width);
        int height = PlayerPrefs.GetInt(resolutionHeight, resolutions[^1].height);
        SetResolution(width, height);

    }

    private void SetResolution(int width, int height)
    {
        string resolutionWidth = "resolutionWidth", resolutionHeight = "resolutionHeight";
        Screen.SetResolution(width, height, Screen.fullScreenMode);
        PlayerPrefs.SetInt(resolutionWidth, width);
        PlayerPrefs.SetInt(resolutionHeight, height);
        PlayerPrefs.Save();
    }

    public void OnResolutionOptionChanged()
    {
        Resolution resolution = resolutions[resolutionDropdown.value];
        SetResolution(resolution.width, resolution.height);
    }

    public void OnScreenModeChanged()
    {
        string screenMode = "screenMode";
        PlayerPrefs.SetInt(screenMode, screenModeDropdown.value);
        PlayerPrefs.Save();
        switch (screenModeDropdown.value)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
        }
    }
}