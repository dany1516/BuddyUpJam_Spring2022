using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLightHandler : MonoBehaviour
{
    [SerializeField] Light fireLight;
    [SerializeField] float time;
    [SerializeField] float minIntensity;
    [SerializeField] float maxIntensity;
    [SerializeField] float valueToChange;

    [SerializeField] float newIntensity;

    void Start()
    {
        newIntensity = fireLight.intensity;
        StartCoroutine("ChangeIntensity");
    }

    IEnumerator ChangeIntensity()
    {
        yield return new WaitForSeconds(time);
        if((int)(newIntensity * 100) == (int)(fireLight.intensity * 100))
        {
            newIntensity = Random.Range(minIntensity, maxIntensity);
            StartCoroutine("ChangeIntensity");
        }
        else
        {
            if(newIntensity < fireLight.intensity){ fireLight.intensity -= valueToChange;}
            else{ fireLight.intensity += valueToChange;} 
            StartCoroutine("ChangeIntensity");
        }
    }
}
