using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InJeneralForestReset : Resetable
{
    [SerializeField] KillerAI killerAI;
    [SerializeField] GameObject KillerArea;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] bool IsAreaActive;
    [SerializeField] bool FinishHouseArea;
    [SerializeField] bool IsForestArea;
    [SerializeField] bool IsMagicalForestArea;
    [SerializeField] EnemyData enemyData;
    [SerializeField] GameObject ForestArea;
    [SerializeField] GameObject MagicalForestArea;
    [SerializeField] BoxCollider SetUpboxCollider;
    [SerializeField] bool IsSetUpBoxCollider;
    public override void Reset()
    {
        ForestArea.SetActive(IsForestArea);
        MagicalForestArea.SetActive(IsMagicalForestArea);
        KillerArea.gameObject.SetActive(IsAreaActive);
        killerAI.transform.position = SpawnPoint.position;
        killerAI.SetState(KillerAI.State.Patrol);
        KillerAI.FinishedHouseArea = FinishHouseArea;
        enemyData.ViewRadius = killerAI.ChangeViewRadiusValue;
        SetUpboxCollider.enabled = IsSetUpBoxCollider;
    }
}
