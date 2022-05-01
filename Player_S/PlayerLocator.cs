using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocator : MonoBehaviour
{
    public static PlayerLocator Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Debug.LogError("Have more than 1 Player");
         
        }
    }
   
}
