using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapDirections : MonoBehaviour
{
    [SerializeField] OpeDoorHingeJoint[] objects;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (var obj in objects)
            {
                obj.flipDragDirection = !obj.flipDragDirection;
                
            }
        }
       
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (var obj in objects)
            {
                obj.flipDragDirection = !obj.flipDragDirection;
            
            }
        }
    }
}
