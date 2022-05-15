using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Settings : MonoBehaviour
{
    [SerializeField] SettingsDataSO settingsData;
    [SerializeField] Slider musicVolume;
    [SerializeField] TextMeshProUGUI musicVolumeText;
    [SerializeField] Slider sFXVolume;
    [SerializeField] TextMeshProUGUI sFXVolumeText;
    [SerializeField] Toggle screenMode;
    [SerializeField] TextMeshProUGUI screenModeText;
    [SerializeField] Toggle helper;
    [SerializeField] TextMeshProUGUI helperText;

    void Start() 
    {
        musicVolume.value = settingsData.GetMusicVolume();
        sFXVolume.value = settingsData.GetSFXVolume();
        screenMode.isOn = settingsData.GetIsFullScreen();
        helper.isOn = settingsData.GetIsEasyMode();

        if(screenMode.isOn) { screenModeText.text = "On";}
        else { screenModeText.text = "Off";}

        if(helper.isOn) { helperText.text = "On";}
        else { helperText.text = "Off";}

        ChangeScreenMode();
    }

    public void AdjustMusicVolume(float volume)
    {
        settingsData.SetMusicVolume(volume);
        musicVolumeText.text = Mathf.RoundToInt(volume * 1000) + "%";
    }

    public void AdjustSFXVolume(float volume)
    {
        settingsData.SetSFXVolume(volume);
        sFXVolumeText.text = Mathf.RoundToInt(volume * 1000) + "%";
    }

    public void AdjustScreenMode(bool screen)
    {
        settingsData.SetIsFullScreen(screen);
        if(screen) { screenModeText.text = "On";}
        else { screenModeText.text = "Off";}
        ChangeScreenMode();
    }

    public void AdjustHelper(bool helper)
    {
        settingsData.SetIsEasyMode(helper);
        if(helper) { helperText.text = "On";}
        else { helperText.text = "Off";}
    }

    public void ChangeScreenMode()
    {
        if(settingsData.GetIsFullScreen())
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
