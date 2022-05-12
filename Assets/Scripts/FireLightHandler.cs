using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLightHandler : MonoBehaviour
{
    [SerializeField] Light light;
    [SerializeField] float time;
    [SerializeField] float minIntensity;
    [SerializeField] float maxIntensity;
    [SerializeField] float valueToChange;

    [SerializeField] float newIntensity;

    void Start()
    {
        newIntensity = light.intensity;
        StartCoroutine("ChangeIntensity");
    }

    IEnumerator ChangeIntensity()
    {
        yield return new WaitForSeconds(time);
        if((int)(newIntensity * 100) == (int)(light.intensity * 100))
        {
            newIntensity = Random.Range(minIntensity, maxIntensity);
            StartCoroutine("ChangeIntensity");
        }
        else
        {
            if(newIntensity < light.intensity){ light.intensity -= valueToChange;}
            else{ light.intensity += valueToChange;} 
            StartCoroutine("ChangeIntensity");
        }
    }
}
