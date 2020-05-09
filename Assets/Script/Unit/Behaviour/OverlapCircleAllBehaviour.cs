using System.Collections;
using System.Collections.Generic;
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


            switch (_nextBehaviourWithTriggerTarget.TriggerTarget)
            {
                case EnumInfo.TriggerTarget.OppositeTeam:
                    if (!_unit._teamType.Equals(_tUnit._teamType))
                        _nextBehaviourWithTriggerTarget.nextBehaviour.DoBehaviour(_tUnit);

                    break;
                case EnumInfo.TriggerTarget.SameTeam:
                    if (_unit._teamType.Equals(_tUnit._teamType))
                        _nextBehaviourWithTriggerTarget.nextBehaviour.DoBehaviour(_tUnit);
                    break;
                case EnumInfo.TriggerTarget.Both:
                        _nextBehaviourWithTriggerTarget.nextBehaviour.DoBehaviour(_tUnit);
                    break;
            }
        }
    }
}

[System.Serializable]
public class NextBehaviourWithTag
{
    public EnumInfo.TriggerTarget TriggerTarget;
    public UnitBehaviour nextBehaviour;

}