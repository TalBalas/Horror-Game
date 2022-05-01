using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAndPull : MonoBehaviour
{
    [SerializeField] Vector3 Axis = Vector3.forward;
    [SerializeField] float MinOffset = 1;
    [SerializeField] float MaxOffset = 1;
    private bool IsHolding;
    [SerializeField] float MaxDistance;
    [SerializeField] GameObject HandIcon;
    private Vector3 StartPosition;
    void Awake()
    {
        StartPosition = transform.localPosition;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void OnMouseOver()
    {
        HandIcon.SetActive((Vector3.Distance(transform.position, PlayerLocator.Instance.transform.position) <= MaxDistance));
    }
   
    void OnMouseExit()
    {
        HandIcon.SetActive(false);
    }
    void OnMouseDrag()
    {
        if (!IsHolding) return;
        transform.localPosition += transform.InverseTransformDirection(Axis) * Input.GetAxis("Mouse Y");
        if (Vector3.Distance(StartPosition, transform.localPosition) * Vector3.Dot(transform.localPosition -StartPosition  , transform.InverseTransformDirection(Axis)) > MaxOffset)
        {
            Debug.LogWarning(StartPosition + transform.InverseTransformDirection(Axis) * MaxOffset);
            transform.localPosition = StartPosition + transform.InverseTransformDirection(Axis) * MaxOffset;
            
        }
        else if (Vector3.Distance(StartPosition, transform.localPosition) * Vector3.Dot(transform.localPosition - StartPosition, transform.InverseTransformDirection(Axis)) < MinOffset)
        {
            Debug.LogWarning(StartPosition + transform.InverseTransformDirection(Axis) * MinOffset + " - " +Vector3.Distance(StartPosition + transform.InverseTransformDirection(Axis) * MinOffset,StartPosition));
           // transform.localPosition = StartPosition + transform.InverseTransformDirection(Axis) * MinOffset;
        }
      //  Debug.Log(Vector3.Distance(StartPosition, transform.localPosition) * Vector3.Dot(transform.localPosition - StartPosition, transform.InverseTransformDirection(Axis)));
    }
    void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, PlayerLocator.Instance.transform.position) > MaxDistance) return;
      //  GameEvents.PushAndPull?.Invoke(true);
        IsHolding = true;
      
    }
    void OnMouseUp()
    {
        IsHolding = false;
       // GameEvents.PushAndPull?.Invoke(false);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position  -transform.TransformDirection(Axis) * MinOffset);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.TransformDirection(Axis) * MaxOffset);
    }
}
