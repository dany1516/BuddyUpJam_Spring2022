using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHandler : MonoBehaviour
{
    [Header("Settings Data")]
    [SerializeField] SettingsDataSO settingsData;

    [Header("Audio source")]
    [SerializeField] AudioSource audioSFX;

    [Header("Pieces SFX")]
    [SerializeField] List<AudioClip> connectSFX;
    [SerializeField] List<AudioClip> pickUpSFX;
    [SerializeField] List<AudioClip> putDownSFX;
    [SerializeField] List<AudioClip> rotateSFX;
    [SerializeField] List<AudioClip> uISFX;

    [Header("Voice reactions dad")]
    [SerializeField] AudioClip dadAh;
    [SerializeField] AudioClip dadHey;
    [SerializeField] AudioClip dadHmm;
    [SerializeField] AudioClip dadOh;

    [Header("Voice reactions girl")]
    [SerializeField] AudioClip girlAh;
    [SerializeField] AudioClip girlHey;
    [SerializeField] AudioClip girHmm;
    [SerializeField] AudioClip girlOh;

    void Start() 
    {
        SetSFXVolume();
    }

    public void PlayConnectSFX()
    {
        var randPickUp = Random.Range(0, putDownSFX.Count - 1);
        audioSFX.PlayOneShot(pickUpSFX[randPickUp]);
    }

    public void PlayPickUpSFX()
    {
        var randConnect = Random.Range(0, connectSFX.Count - 1);
        audioSFX.PlayOneShot(connectSFX[randConnect]);
    }

    public void PlayPutDownSFX()
    {
        var randPutDown = Random.Range(0, putDownSFX.Count - 1);
        audioSFX.PlayOneShot(putDownSFX[randPutDown]);
    }

    public void PlayRotateSFX()
    {
        var randPutDown = Random.Range(0, rotateSFX.Count - 1);
        audioSFX.PlayOneShot(rotateSFX[randPutDown]);
    }

    public void PlayUISFX()
    {
        var randPutDown = Random.Range(0, uISFX.Count - 1);
        audioSFX.PlayOneShot(uISFX[randPutDown]);
    }

    public void PlayDadAh(){audioSFX.PlayOneShot(dadAh);}
    public void PlayDadHey(){audioSFX.PlayOneShot(dadHey);}
    public void PlayDadHmm(){audioSFX.PlayOneShot(dadHmm);}
    public void PlayDadOh(){audioSFX.PlayOneShot(dadOh);}
    public void PlayGirlAh(){audioSFX.PlayOneShot(girlAh);}
    public void PlayGirlHey(){audioSFX.PlayOneShot(girlHey);}
    public void PlayGirlHmm(){audioSFX.PlayOneShot(girHmm);}
    public void PlayGirlOh(){audioSFX.PlayOneShot(girlOh);}
    
    public void SetSFXVolume(){audioSFX.volume = settingsData.GetSFXVolume();}
}
