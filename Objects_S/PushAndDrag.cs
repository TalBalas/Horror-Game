using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAndDrag : Resetable
{
    private enum State { Static, Physics }
    [SerializeField] Transform Holdlocator;
    [SerializeField] bool KeepMomentumOnRelease;
    [SerializeField] float HoldSpeed;
    [SerializeField] float MaxDistance;
    private Vector3 StartPosition;
    private Vector3 StartRotation;
    private bool IsClose; 
    private State States;
    [SerializeField] Rigidbody rb;
    void Awake()
    {
        States = State.Physics;
        StartPosition = transform.localPosition;
        StartRotation = transform.localRotation.eulerAngles;
   
    }
   
    void OnMouseDrag()
    {
       /* if (Mathf.Abs(OffsetValue) >= PhysicsOffest)
        {
            States = State.Physics;
        }*/
        

      /*  if (States == State.Static)
        {
            var axis = Input.GetAxis(AxisDirection);
            rb.isKinematic = true;
            OffsetValue += axis * Speed;
            OffsetValue = Mathf.Clamp(OffsetValue, 0, MaxOffsetValue);
            transform.position = StartPosition + OffsetVector * OffsetValue;
            
        }   /*/   
        if (States == State.Physics)
        {
            GameEvents.HoldingObject?.Invoke(gameObject);
            rb.velocity = (Holdlocator.position - transform.position) * HoldSpeed;
            rb.angularVelocity = (Holdlocator.position - transform.position) * HoldSpeed;
            HandleRBvalues();
        }
      
    }
    private void HandleRBvalues()
    {
        if(States == State.Physics)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.FreezeRotationX;
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            
        }
        else
        {
            rb.interpolation = RigidbodyInterpolation.None;
            rb.useGravity = true;
        }
    }
   void OnMouseUp()
    {
        // IsHolding = false;
        GameEvents.HoldingObject?.Invoke(null);
        if (!KeepMomentumOnRelease)
        {
            rb.velocity = Vector3.zero;
        }
        HandleRBvalues();

    }
    public override void Reset()
    {
        transform.localPosition = StartPosition;
        transform.localRotation = Quaternion.Euler(StartRotation);
    }
}
