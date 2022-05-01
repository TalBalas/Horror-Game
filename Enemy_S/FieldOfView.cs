using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
public class FieldOfView : Resetable
{
    [SerializeField] bool IsVisible;
    public EnemyData enemyData;
    public LayerMask TargetMask;
    public LayerMask obstacleMask;

    public override void Reset()
    {
        IsVisible = false;
        enemyData.ViewRadius = 8;
        this.GetComponent<FieldOfView>().enabled = true;
    }
    void OnEnable()
    {
        StartCoroutine(FindsTargetWithDelay(0.2f));
    }
    public Vector3 DirFromAngle(float angleDegress ,bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleDegress += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleDegress * Mathf.Deg2Rad), 0, Mathf.Cos(angleDegress * Mathf.Deg2Rad));
    }
  
    private IEnumerator FindsTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    void FindVisibleTargets()
    {
        Vector3 finalTarget = PlayerLocator.Instance.transform.position + Vector3.up * enemyData.SpherecastTargetOffset;
        Vector3 finalOrigin = transform.position + Vector3.up * enemyData.SpherecastOriginOffset;
        Vector3 distanceToTarget = finalTarget - finalOrigin;


        if (distanceToTarget.magnitude < enemyData.MinViewDistance)
        {
            Visible();
            return;
        }
        if(distanceToTarget.magnitude > enemyData.ViewRadius) 
        {
          
            Invisible();
            return;
        }
        if (!(Vector3.Angle(transform.forward, distanceToTarget.normalized) < enemyData.ViewAngle / 2)) // if player not in angel 
        {
            Invisible();
            return;
        } 

        if (!Physics.SphereCast(finalOrigin, enemyData.SpherecastRadius, distanceToTarget.normalized,out var hit, distanceToTarget.magnitude, obstacleMask))
        {
            Visible();
           
        }
        else
        {
            Invisible();
         
            return;
        }
    }
    private void Invisible()
    {
        if (IsVisible)
        {
            IsVisible = false;
          
            GameEvents.Visible?.Invoke(false);
        }
    }
    private void Visible()
    {
        if (!IsVisible)
        {
            IsVisible = true;
            Debug.Log("Found Player");
            Debug.DrawLine(transform.position + Vector3.up * enemyData.SpherecastOriginOffset, PlayerLocator.Instance.transform.position + Vector3.up * enemyData.SpherecastTargetOffset,Color.green,10);
            GameEvents.Visible?.Invoke(true);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.up * enemyData.SpherecastOriginOffset, enemyData.SpherecastRadius);
    }
}
