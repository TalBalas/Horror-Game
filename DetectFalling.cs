using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectFalling : MonoBehaviour
{
    [SerializeField] bool IsLeft;
    [SerializeField] PlayerMovment playerMovment;
    void OnTriggerEnter(Collider other)
    {
        if (other is CharacterController) return;
       
        playerMovment.TouchedFallTrigger(IsLeft);
      
    }
}
