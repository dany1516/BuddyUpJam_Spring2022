using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [Header("Audio source")]
    [SerializeField] AudioSource audio;

    [Header("Pieces SFX")]
    [SerializeField] List<AudioClip> connectSFX;
    [SerializeField] List<AudioClip> pickUpSFX;
    [SerializeField] List<AudioClip> putDownSFX;
    [SerializeField] List<AudioClip> rotateSFX;

    public void PlayConnectSFX()
    {
        var randPickUp = Random.Range(0, putDownSFX.Count - 1);
        audio.PlayOneShot(pickUpSFX[randPickUp]);
    }

    public void PlayPickUpSFX()
    {
        var randConnect = Random.Range(0, connectSFX.Count - 1);
        audio.PlayOneShot(connectSFX[randConnect]);
    }

    public void PlayPutDownSFX()
    {
        var randPutDown = Random.Range(0, putDownSFX.Count - 1);
        audio.PlayOneShot(putDownSFX[randPutDown]);
    }

    public void PlayRotateSFX()
    {
        var randPutDown = Random.Range(0, rotateSFX.Count - 1);
        audio.PlayOneShot(rotateSFX[randPutDown]);
    }
}
