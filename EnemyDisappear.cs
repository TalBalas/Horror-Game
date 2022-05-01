using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisappear : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    [SerializeField] GameObject KillerHouse;
    void Update()
    {
        if(KillerAI.FinishedHouseArea && Vector3.Distance(PlayerLocator.Instance.transform.position,this.transform.position) > enemyData.EnemyDisappearDistance)
        {
            KillerHouse.gameObject.SetActive(false);
        }
    }
}
