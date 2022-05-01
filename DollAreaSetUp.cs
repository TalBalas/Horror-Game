using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollAreaSetUp : AreaSetUp 
{
    [SerializeField] Transform ObjectToLook;
    [SerializeField] bool FreezeCameraInvoke;
   
    public override void Reset()
    {
        GameEvents.LaserAction?.Invoke(false);
        GameEvents.TurnAround?.Invoke(new GameEvents.TurnAroundEvenetData { ObjectToTurn = null, time = 0, IsTurnAndNotBack = false, FreezCamera = false });
        StartCoroutine(ColliderEnable());
    }
    IEnumerator ColliderEnable()
    {
        yield return new WaitForSeconds(0.5f);
        boxCollider.enabled = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetUp();
            Debug.Log(other.gameObject.name);
        }

    }

    private void SetUp()
    {
        base.ActiveArea();
        GameEvents.LaserAction?.Invoke(true);
        GameEvents.TurnAround?.Invoke(new GameEvents.TurnAroundEvenetData { ObjectToTurn = ObjectToLook, time = 1, IsTurnAndNotBack = true, FreezCamera = FreezeCameraInvoke });
    }
}
