using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to control the light flickering in accordance to gun shots
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class LightEmission : MonoBehaviour {

    private float lightFlickerTime;
    private float lightFlickerTimer;
    private float flickerDuration;
    private float flickerOverallTime;

    // This is used to initialise the light flicker
    void OnEnable()
    {
        ResetFlicker();
    }

    // This function resets the flicker and uses the emission rate of gun particle system to time the flicker with gunshot
    public void ResetFlicker () {
        lightFlickerTime = 1/ GetComponentInParent<ParticleSystem>().emission.rateOverTime.constant;
        lightFlickerTimer = lightFlickerTime;
        flickerDuration = GetComponentInParent<ParticleSystem>().main.duration;
    }

    // Update is called once per frame
    void Update()
    {
        lightFlickerTimer -= Time.deltaTime;
        flickerOverallTime += Time.deltaTime;

        // This part turns on or off the light depending on the previous state
        if (lightFlickerTimer <= 0)
        {
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
            lightFlickerTimer = lightFlickerTime;
        }

        // This turns off the light completely if the particle system is stopped or if it's been on for more than the duration of particle system
        if (flickerOverallTime >= flickerDuration || !GetComponentInParent<ParticleSystem>().isEmitting)
        {
            GetComponent<Light>().enabled = false;
            enabled = false;
            flickerOverallTime = 0;
        }
    }
}
