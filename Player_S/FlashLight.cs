using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] GameObject LightOfFlashLight;
    [SerializeField] bool IsLighitingOn;
    [SerializeField] AudioSource FlashlightSound;
    public static bool IsFlashlightOn;
    void Update()
    {
        TurnOnAndOff();
    }

    private void TurnOnAndOff()
    {
        if (Input.GetKeyDown(KeyCode.R) && !IsLighitingOn)
        {
            IsFlashlightOn = true;
            LightOfFlashLight.SetActive(true);
            IsLighitingOn = true;
            FlashlightSound.Play();
        }
        else if (Input.GetKeyDown(KeyCode.R) && IsLighitingOn)
        {
            IsFlashlightOn = false;
            IsLighitingOn = false;
            LightOfFlashLight.SetActive(false);
            FlashlightSound.Play();
        }

     
    }
   
}
