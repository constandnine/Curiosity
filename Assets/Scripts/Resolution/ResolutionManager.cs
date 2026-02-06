using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private FullScreenMode fullScreenMode = FullScreenMode.FullScreenWindow;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions = new List<Resolution>();


    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            Resolution resolution = resolutions[i];

            if (filteredResolutions.Exists(r => r.width == resolution.width && r.height == resolution.height)) continue;

            filteredResolutions.Add(resolution);

            string option = resolution.width + " x " + resolution.height;
            options.Add(option);

            if (resolution.width == Screen.currentResolution.width &&
                resolution.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = filteredResolutions.Count - 1;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        SetResolution(currentResolutionIndex);
    }

    public void SetResolution(int index)
    {
        Resolution res = filteredResolutions[index];
        Screen.SetResolution(res.width, res.height, fullScreenMode);
        Debug.Log("Resolution set");
    }

    public void SetFullscreen(bool incomingValue)
    {
        Debug.Log(incomingValue);
        Screen.fullScreenMode = fullScreenMode; 
        Screen.fullScreen = incomingValue;
    }
}