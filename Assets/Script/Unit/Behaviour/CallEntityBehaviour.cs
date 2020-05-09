using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEntityBehaviour : UnitBehaviour
{
    [SerializeField] private string CallEntityIdx;

    [SerializeField] private Transform ShootTr;
    [Header("SpawnTr없으면 Entity로 타겟 유닛 전달")]
    [SerializeField] private Transform TargetTr;


    public override void DoBehaviour()
    {
        EntityManager.Instance.CallEntity(CallEntityIdx, _unit, ShootTr.position, TargetTr.position);
    }

    public override void DoBehaviour(Unit _tUnit)
    {

        EntityManager.Instance.CallEntity(CallEntityIdx, _unit, ShootTr.position, _tUnit);
    }
}
