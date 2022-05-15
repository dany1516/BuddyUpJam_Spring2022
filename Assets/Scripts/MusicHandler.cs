using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [Header("Settings Data")]
    [SerializeField] SettingsDataSO settingsData;

    [Header("Songs")]
    [SerializeField] AudioSource mainSong;
    [SerializeField] AudioSource oneAndFourSong;
    [SerializeField] AudioSource twoAndThreeSong;
    [SerializeField] AudioSource fiveAndSixSong;
    [SerializeField] AudioSource sevenAndEightSong;
    [SerializeField] AudioSource nineAndTenSong;
    
    private void Start() 
    {
        SetMusicVolume();
    }

    void MuteAll()
    {
        mainSong.mute = true;
        oneAndFourSong.mute = true;
        twoAndThreeSong.mute = true;
        fiveAndSixSong.mute = true;
        sevenAndEightSong.mute = true;
        nineAndTenSong.mute = true;
    }

    public void StartMainSong()
    {
        MuteAll();
        mainSong.mute = false;
    }

    public void StartOneAndFourSong()
    {
        MuteAll();
        oneAndFourSong.mute = false;
    }

    public void StartTwoAndThreeSong()
    {
        MuteAll();
        twoAndThreeSong.mute = false;
    }

    public void StartFiveAndSixSong()
    {
        MuteAll();
        fiveAndSixSong.mute = false;
    }

    public void StartSevenAndEightSongSong()
    {
        MuteAll();
        sevenAndEightSong.mute = false;
    }

    public void StartNineAndTenSong()
    {
        MuteAll();
        nineAndTenSong.mute = false;
    }

    public void SetMusicVolume()
    {
        mainSong.volume = settingsData.GetMusicVolume();
        oneAndFourSong.volume = settingsData.GetMusicVolume();
        twoAndThreeSong.volume = settingsData.GetMusicVolume();
        fiveAndSixSong.volume = settingsData.GetMusicVolume();
        sevenAndEightSong.volume = settingsData.GetMusicVolume();
        nineAndTenSong.volume = settingsData.GetMusicVolume();
    }
}
