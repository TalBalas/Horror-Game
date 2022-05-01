using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour
{
   /* public enum ObjectsType { Jerrican, SquareWood };
    public ObjectsType ObjectTypes;
    [SerializeField] TakeMassage takeMassage;
    [SerializeField] Transform HoldPosition;
    [SerializeField] GameObject boxCollider;
    [SerializeField] GameObject boxTrigger;
    [SerializeField] Rigidbody RB;
    [SerializeField] ObjectType objectType;
    [SerializeField] GameObject[] HoldObjects;
    public bool IsTook;

    public void HoldAndDropJerrican()
    {

        if (IsTook)
        {
            TakeJerrican();
        } 
        else
        {

            DropObject();
        }
    }
    public void HoldAndDropQuareWood()
    {
        if (IsTook) 
        {
            TakeWood();
        } 
        else
        {

            DropObject();
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && takeMassage.IsClose && !IsTook)
        {
            GameEvents.HoldingObject?.Invoke(true);
          
        }
        else if (Input.GetKeyUp(KeyCode.E) && !takeMassage.IsClose && IsTook)
        {
            GameEvents.HoldingObject?.Invoke(false);
          
        }
    }
  
    void OnEnable()
    {
        GameEvents.HoldingObject += OnTakeObject;

    }
    void OnDestroy()
    {
        GameEvents.HoldingObject -= OnTakeObject;
    }
    private void OnTakeObject(bool isTake)
    { 
        IsTook = isTake;
    }
    private void TakeJerrican()
    {
      
        HoldObjects[0].transform.position = HoldPosition.position;
        DeafultTake();
    }
    private void TakeWood()
    {
       
        HoldObjects[1].transform.position = HoldPosition.position;
        DeafultTake();
    }
    private void DeafultTake()
    {
        RB.interpolation = RigidbodyInterpolation.Interpolate;
        takeMassage.IsClose = false;
        boxCollider.SetActive(false);
        RB.isKinematic = true;
    }
    private void DropObject()
    {
        boxCollider.SetActive(true);   
        RB.isKinematic = false;
        RB.interpolation = RigidbodyInterpolation.None;
   
    }*/
}
