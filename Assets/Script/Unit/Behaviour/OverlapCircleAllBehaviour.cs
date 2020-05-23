using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OverlapCircleAllBehaviour : UnitBehaviour
{
    public NextBehaviourWithTag _nextBehaviourWithTriggerTarget;

    public float _radius;
    public Transform _pointTr;



    public override void InitUnitBehaviour(Unit _u)
    {
        base.InitUnitBehaviour(_u);

        if (_nextBehaviourWithTriggerTarget.nextBehaviour != null)
            _nextBehaviourWithTriggerTarget.nextBehaviour.InitUnitBehaviour(_u);

    }
    public override void StartBattle()
    {
        if (_nextBehaviourWithTriggerTarget.nextBehaviour)
            _nextBehaviourWithTriggerTarget.nextBehaviour.StartBattle();
    }
    public override void DoBehaviour()
    {
        ProcessOerlapCircle( Physics2D.OverlapCircleAll(_pointTr.position, _radius));
    }

    public override void DoBehaviour(Unit _tUnit)
    {
        ProcessOerlapCircle( Physics2D.OverlapCircleAll(_tUnit._tr.position, _radius));
    }

    void ProcessOerlapCircle(Collider2D[] _cols)
    {
        for(int i =0;i< _cols.Length; i++)
        {
            if (!_cols[i].CompareTag("Unit"))
                continue;

            Unit _tUnit = _cols[i].GetComponent<Unit>();


            switch (_nextBehaviourWithTriggerTarget.TargetTeam)
            {
                case EnumInfo.TargetTeam.OppositeTeam:
                    if (!_unit._teamType.Equals(_tUnit._teamType))
                        _nextBehaviourWithTriggerTarget.nextBehaviour.DoBehaviour(_tUnit);

                    break;
                case EnumInfo.TargetTeam.SameTeam:
                    if (_unit._teamType.Equals(_tUnit._teamType))
                        _nextBehaviourWithTriggerTarget.nextBehaviour.DoBehaviour(_tUnit);
                    break;
                case EnumInfo.TargetTeam.Both:
                        _nextBehaviourWithTriggerTarget.nextBehaviour.DoBehaviour(_tUnit);
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}

[System.Serializable]
public class NextBehaviourWithTag
{
    public EnumInfo.TargetTeam TargetTeam;
    public UnitBehaviour nextBehaviour;

}