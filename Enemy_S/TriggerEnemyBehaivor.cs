using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemyBehaivor : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Work");
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy");
        }
    }
}
