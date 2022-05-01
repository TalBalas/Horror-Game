using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InJeneralMagicalForestReset : Resetable
{
    [SerializeField] WalkToTarget walkToTarget;
    [SerializeField] GameObject DollArea;
    [SerializeField] Transform DollSpawnPoint;
    [SerializeField] bool IsAreaActive;
    
    public override void Reset()
    {
        DollArea.gameObject.SetActive(IsAreaActive);
        walkToTarget.transform.position = DollSpawnPoint.position;
    }
}
