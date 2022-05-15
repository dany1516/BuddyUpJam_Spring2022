using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Settings", menuName = "Create New Settings File", order = 3)]
public class SettingsDataSO : ScriptableObject 
{
    [SerializeField] [Range(0,.1f)] float musicVolume;
    [SerializeField] [Range(0,.1f)] float sFXVolume;
    [SerializeField] bool isFullScreen;
    [SerializeField] bool isEasyMode;

    public float GetMusicVolume() => musicVolume;
    public float GetSFXVolume() => sFXVolume;
    public bool GetIsFullScreen() => isFullScreen;
    public bool GetIsEasyMode() => isEasyMode;

    public void SetMusicVolume(float volume) { musicVolume = volume;}
    public void SetSFXVolume(float volume) { sFXVolume = volume;}
    public void SetIsFullScreen(bool screen) { isFullScreen = screen;}
    public void SetIsEasyMode(bool mode) { isEasyMode = mode;}
}
