using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPullObject : Resetable
{
    [SerializeField] GameObject HandIcon;
    [SerializeField] float Speed;
    [SerializeField] float WalkBackSpeed;
    [SerializeField] float MaxDistance;
    [SerializeField] float MaxOffsetValue;
    [SerializeField] string AxisDirection;
    [SerializeField] Vector3 OffsetVector;
    private float OffsetDelta;
    private float OffsetValue;
    private Vector3 StartRotaion;
    private bool IsClose;
  
    void Awake()
    {
        StartRotaion = transform.localRotation.eulerAngles;
      
    }
    void OnMouseOver()
    {
        if (DragObject.IsHoldingGlobal) return;
        IsClose = (Vector3.Distance(transform.position, PlayerLocator.Instance.transform.position) <= MaxDistance);
        HandIcon.SetActive(IsClose);
    }
    
    void OnMouseDrag()
    {
        IsClose = (Vector3.Distance(transform.position, PlayerLocator.Instance.transform.position) <= MaxDistance);
        if (!IsClose) 
        {
            HandIcon.SetActive(IsClose);
            DragObject.IsHoldingGlobal = false;
            return;
        } 
        DragObject.IsHoldingGlobal = true;
       
         OffsetDelta = 0;
         DragMouseAxis();
        OffsetValue += OffsetDelta;
        OffsetValue = Mathf.Clamp(OffsetValue, 0, MaxOffsetValue);
        transform.localRotation = Quaternion.Euler(StartRotaion + OffsetVector * OffsetValue);
    }
    
    private void DragMouseAxis()
    {
        var axis = Input.GetAxis(AxisDirection);
        OffsetDelta += axis * Speed;
    }
   
    void OnMouseExit()
    {
        if (DragObject.IsHoldingGlobal) return;
        HandIcon.SetActive(false);
    }
    void OnMouseUp()
    {
        DragObject.IsHoldingGlobal = false;
        HandIcon.SetActive(false);
       
    }
    void OnMouseDown()
    {
        if (!IsClose) return;
        HandIcon.SetActive(IsClose);
    }

    public override void Reset()
    {
        transform.localRotation = Quaternion.Euler(StartRotaion);
        OffsetValue = 0;
    }
}
