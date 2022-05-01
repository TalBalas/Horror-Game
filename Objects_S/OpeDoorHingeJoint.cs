using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeDoorHingeJoint : Resetable
{
  
    [SerializeField] VisibleHandIcon visibleHand;
    [SerializeField] Rigidbody rb;
    [SerializeField] float Speed;
    [SerializeField] float WalkBackSpeed;
    [SerializeField] Vector3 OffsetVector;
    public bool flipDragDirection;
    public bool IsLock;
    public bool IsCreckingSound;
    [SerializeField] float MinOffsetValue;
    [SerializeField] float MaxOffsetValue;
    [SerializeField] AudioSource DoorCreaking;
    private float LastOffsetDelta;
    private Vector3 StartRotation;
    private Quaternion OriginalRotation;
    private float OffsetDelta;
    private float OffsetValue;
    private Vector3 dirDoor;
    void Awake()
    {
      
        StartRotation = transform.localRotation.eulerAngles;
        OriginalRotation = transform.localRotation;
    }
    void OnMouseDown()
    {
        if (!visibleHand.IsActive ||IsLock) return;
        OffsetValue = 0;
        rb.isKinematic = true;
        StartRotation = transform.localRotation.eulerAngles;
            
        visibleHand.HandIcon.SetActive(visibleHand.IsActive);

    }
    
    void OnMouseDrag()
    {
        if (IsLock && visibleHand.IsActive) 
        {
            return;
        } 
        if (!visibleHand.IsActive)
        {
           visibleHand.HandIcon.SetActive(visibleHand.IsActive);
            DragObject.IsHoldingGlobal = false;
            return;
        }
        DragObject.IsHoldingGlobal = true;
        if (IsCreckingSound)
        {
            DoorCreaking.Play();
        }
      
        OffsetDelta = 0;
        DragMouseAxis();
        BackFowardOpenDoor();
        LeftRightOpenDoor();
        OffsetValue += OffsetDelta;
        dirDoor = StartRotation + OffsetVector * OffsetValue;
        dirDoor.y = Mathf.Clamp(dirDoor.y, MinOffsetValue, MaxOffsetValue);
        transform.localRotation = Quaternion.Euler(dirDoor);
        LastOffsetDelta = OffsetDelta;
    }
   
   
    
    void OnMouseUp()
    {
        if (IsLock) return;
        DragObject.IsHoldingGlobal = false;
        rb.isKinematic = false;
        if(dirDoor.y != MinOffsetValue && dirDoor.y != MaxOffsetValue) 
            rb.angularVelocity = OffsetVector * LastOffsetDelta;
       
       visibleHand.HandIcon.SetActive(false);

    }
    private void DragMouseAxis()
    {
          var axis = Input.GetAxis("Mouse X") * (flipDragDirection ? -1 : 1);
     
        OffsetDelta += axis * Speed;
    }
    private void BackFowardOpenDoor()
    {
        var vert = Input.GetAxis("Vertical");
    
        OffsetDelta += vert * WalkBackSpeed;
    }
    private void LeftRightOpenDoor()
    {
        var horiz = Input.GetAxis("Horizontal");
      
        OffsetDelta += horiz * WalkBackSpeed;
    }

    public override void Reset()
    {
        transform.localRotation = OriginalRotation;
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = false;
    }
  
}