using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUnitCountBehaviour : CommonBehaviour
{
    public int _targetCount;
    public EnumInfo.TargetTeam _targetTeam;

    public BehaviourDependOnRange _nextBehaviour;

    public override void InitUnitBehaviour(Unit _u)
    {
        base.InitUnitBehaviour(_u);

        if (_nextBehaviour!=null)
        {
            _nextBehaviour.NextBehaviourWhenTrue.InitUnitBehaviour(_u);
            _nextBehaviour.NextBehaviourWhenFalse.InitUnitBehaviour(_u);
        }
    }
    public override void StartBattle()
    {
        if (_nextBehaviour != null)
        {
            _nextBehaviour.NextBehaviourWhenTrue.StartBattle();
            _nextBehaviour.NextBehaviourWhenFalse.StartBattle();
        }
    }

    public override void DoBehaviour()
    {
        Debug.Log("DoBehaviour()");
        List<Unit> _list = null;
        if (_targetTeam.Equals(EnumInfo.TargetTeam.SameTeam))
        {
            Debug.Log("DoBehaviour() 1");
            _list = UnitManager.Instance.GetAliveUnitList(_unit._teamType);
        }
        else if (_targetTeam.Equals(EnumInfo.TargetTeam.OppositeTeam))
        {
            Debug.Log("DoBehaviour() 2");
            _list = UnitManager.Instance.GetAliveUnitList(_unit._teamType.Equals(EnumInfo.TeamType.Player) ? EnumInfo.TeamType.Opposite : EnumInfo.TeamType.Player);
        }
        else
        {
            Debug.Log("DoBehaviour() 3 ");
            _list = UnitManager.Instance._curAliveUnitsOnTile;
        }

        CheckNextBehaviourDependOnRange(_list);
    }

    public override void DoBehaviour(Unit _tUnit)
    {

        Debug.Log("DoBehaviour(Unit _tUnit)");
        List<Unit> _list = null;
        if (_targetTeam.Equals(EnumInfo.TargetTeam.SameTeam))
        {
            Debug.Log("DoBehaviour(Unit _tUnit) 1");
            _list = UnitManager.Instance.GetAliveUnitList(_tUnit._teamType);
        }
        else if (_targetTeam.Equals(EnumInfo.TargetTeam.OppositeTeam))
        {
            Debug.Log("DoBehaviour(Unit _tUnit) 2");
            _list = UnitManager.Instance.GetAliveUnitList(_tUnit._teamType.Equals(EnumInfo.TeamType.Player) ? EnumInfo.TeamType.Opposite : EnumInfo.TeamType.Player);
        }
        else
        {
            Debug.Log("DoBehaviour(Unit _tUnit) 3");
            _list = UnitManager.Instance._curAliveUnitsOnTile;
        }

        CheckNextBehaviourDependOnRange(_list, _tUnit);
    }

    void CheckNextBehaviourDependOnRange(List<Unit> _list)
    {
        switch (_nextBehaviour.RangeExpression)
        {
            case EnumInfo.RangeExpression.Less: // 미만

                if (_targetCount < _list.Count)
                    _nextBehaviour.NextBehaviourWhenTrue.DoBehaviour();
                else
                    _nextBehaviour.NextBehaviourWhenFalse.DoBehaviour();

                break;
            case EnumInfo.RangeExpression.OrLess: // 이하

                if (_targetCount <= _list.Count)
                    _nextBehaviour.NextBehaviourWhenTrue.DoBehaviour();
                else
                    _nextBehaviour.NextBehaviourWhenFalse.DoBehaviour();
                break;
            case EnumInfo.RangeExpression.OrMore: // 이상

                if (_targetCount >= _list.Count)
                    _nextBehaviour.NextBehaviourWhenTrue.DoBehaviour();
                else
                    _nextBehaviour.NextBehaviourWhenFalse.DoBehaviour();
                break;
            case EnumInfo.RangeExpression.Over: // 초과

                if (_targetCount > _list.Count)
                {
                    Debug.Log("CheckNextBehaviourDependOnRange 1");
                    _nextBehaviour.NextBehaviourWhenTrue.DoBehaviour();
                }
                else
                {
                    Debug.Log("CheckNextBehaviourDependOnRange 2");
                    _nextBehaviour.NextBehaviourWhenFalse.DoBehaviour();
                }
                break;
            case EnumInfo.RangeExpression.Same: // 같음

                if (_targetCount == _list.Count)
                    _nextBehaviour.NextBehaviourWhenTrue.DoBehaviour();
                else
                    _nextBehaviour.NextBehaviourWhenFalse.DoBehaviour();
                break;
            case EnumInfo.RangeExpression.Default:
                _nextBehaviour.NextBehaviourWhenTrue.DoBehaviour();
                break;
        }
    }

    void CheckNextBehaviourDependOnRange(List<Unit> _list, Unit _tUnit)
    {
        switch (_nextBehaviour.RangeExpression)
        {
            case EnumInfo.RangeExpression.Less: // 미만

                if (_targetCount < _list.Count)
                    _nextBehaviour.NextBehaviourWhenTrue.DoBehaviour(_tUnit);
                else
                    _nextBehaviour.NextBehaviourWhenFalse.DoBehaviour(_tUnit);

                break;
            case EnumInfo.RangeExpression.OrLess: // 이하

                if (_targetCount <= _list.Count)
                    _nextBehaviour.NextBehaviourWhenTrue.DoBehaviour(_tUnit);
                else
                    _nextBehaviour.NextBehaviourWhenFalse.DoBehaviour(_tUnit);
                break;
            case EnumInfo.RangeExpression.OrMore: // 이상

                if (_targetCount >= _list.Count)
                    _nextBehaviour.NextBehaviourWhenTrue.DoBehaviour(_tUnit);
                else
                    _nextBehaviour.NextBehaviourWhenFalse.DoBehaviour(_tUnit);
                break;
            case EnumInfo.RangeExpression.Over: // 초과
                if (_targetCount > _list.Count)
                {
                    Debug.Log("CheckNextBehaviourDependOnRange 1");
                    _nextBehaviour.NextBehaviourWhenTrue.DoBehaviour(_tUnit);
                }
                else
                {
                    Debug.Log("CheckNextBehaviourDependOnRange 2");
                    _nextBehaviour.NextBehaviourWhenFalse.DoBehaviour(_tUnit);
                }
                break;
            case EnumInfo.RangeExpression.Same: // 같음

                if (_targetCount == _list.Count)
                    _nextBehaviour.NextBehaviourWhenTrue.DoBehaviour(_tUnit);
                else
                    _nextBehaviour.NextBehaviourWhenFalse.DoBehaviour(_tUnit);
                break;
            case EnumInfo.RangeExpression.Default:
                _nextBehaviour.NextBehaviourWhenTrue.DoBehaviour(_tUnit);
                break;
        }
    }

}

[System.Serializable]
public class BehaviourDependOnRange
{
    public EnumInfo.RangeExpression RangeExpression;
    public UnitBehaviour NextBehaviourWhenTrue;
    public UnitBehaviour NextBehaviourWhenFalse;
}