using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEntityBehaviour : UnitBehaviour
{
    [SerializeField] private string CallEntityIdx;

    [Header("SpawnTr없으면 Entity로 타겟 유닛 전달")]
    [SerializeField] private Transform SpawnTr;


    public override void DoBehaviour()
    {
        Debug.Log("DoBehaviour() CallEntityIdx : " + CallEntityIdx); 
        EntityManager.Instance.CallEntity(CallEntityIdx, _unit, SpawnTr.position);
    }

    public override void DoBehaviour(Unit _tUnit)
    {
        Debug.Log("DoBehaviour(Unit) CallEntityIdx : " + CallEntityIdx);
        EntityManager.Instance.CallEntity(CallEntityIdx, _unit, _tUnit);
    }
}
