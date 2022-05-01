using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(PlayerLocator.Instance.transform.position);
    }
}
