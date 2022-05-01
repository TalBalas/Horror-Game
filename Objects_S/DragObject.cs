using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : Resetable
{
    private Rigidbody rb;
    private bool IsHolding;
    public static bool IsHoldingGlobal;
    [SerializeField] GameObject HandIcon;
    [SerializeField] Transform Holdlocator;
    [SerializeField] PlayerData playerData;
    [SerializeField] bool KeepMomentumOnRelease;
    [SerializeField] bool IsHeavy;
    [SerializeField] AudioSource FallBottle;
    private Vector3 StartPosition;
    private Vector3 StartRotation;
    void Awake()
    {
       rb =  GetComponent<Rigidbody>();
   
        StartPosition = transform.localPosition;
        StartRotation = transform.localRotation.eulerAngles;
    }
   
    void OnMouseOver()
    {
        if (IsHoldingGlobal) return;
        HandIcon.SetActive((Vector3.Distance(transform.position, PlayerLocator.Instance.transform.position) <= playerData.MaxDistance));
    }
    void OnMouseExit()
    {
        if (IsHoldingGlobal) return;
        HandIcon.SetActive(false);
    }
    void OnMouseDrag()
    {
        DragObjects();
    }
    void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, PlayerLocator.Instance.transform.position) > playerData.MaxDistance) return;
        IsHolding = true;
        IsHoldingGlobal = true;
        HandIcon.SetActive(true);
        GameEvents.HoldingObject?.Invoke(gameObject);
         rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.useGravity = false;
    }
    void OnMouseUp()
    {
        RealseObject();
    }
    void FixedUpdate()
    {
        if (IsHolding)
        {
            transform.LookAt(Camera.main.transform.position - transform.position);
        }
      
    }
    
    private void DragObjects()
    {
        if (!IsHolding) return;
      //  rb.transform.position += Holdlocator.position - playerPos;
      //  playerPos = PlayerLocator.Instance.transform.position;    
        rb.velocity = (Holdlocator.position - transform.position) * playerData.Speed;
        rb.angularVelocity = (Holdlocator.position - transform.position) * playerData.Speed;
        transform.LookAt(Camera.main.transform.position);
        if (IsHeavy && transform.position.y >= playerData.MaxHighYtransform)
        {
            RealseObject();
        }

        
    }
    private void RealseObject()
    {
        IsHolding = false;
        IsHoldingGlobal = false;
        HandIcon.SetActive(false);
        GameEvents.HoldingObject?.Invoke(null);
        if (!KeepMomentumOnRelease)
        {
            rb.velocity = Vector3.zero;
        }
       
        rb.interpolation = RigidbodyInterpolation.None;
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        float dis = Vector3.Distance(transform.position, PlayerLocator.Instance.transform.position);
        if (dis < 5)
        {
            FallBottle.Play();
        }
    }
    public override void Reset()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.velocity = Vector3.zero;
        IsHolding = false;
        IsHoldingGlobal = false;
        rb.useGravity = false;
        transform.localPosition = StartPosition;
        transform.localRotation = Quaternion.Euler(StartRotation);
      
    }
}
