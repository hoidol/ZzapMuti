using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnitOnTileBehaviour : CommonBehaviour
{
    public EnumInfo.SelectUnitType _getUnitType;
    public EnumInfo.TargetTeam _targetTeam;
    public UnitBehaviour _nextBehaviour;
    public bool _includeSelf;
    public int _count;
    //List<Unit> _alrealySelectedUnitList = new List<Unit>();
    public override void InitUnitBehaviour(Unit _u)
    {
        base.InitUnitBehaviour(_u);
        if (_nextBehaviour)
            _nextBehaviour.InitUnitBehaviour(_u);
    }
    public override void StartBattle()
    {
        if (_nextBehaviour)
            _nextBehaviour.StartBattle();
    }
    public override void DoBehaviour()
    {
        SetAliveUnit();
        for (int i =0;i< _count; i++)
        {
            if (_curAliveUnitList.Count <= 0)
                break;

            switch (_getUnitType)
            {
                case EnumInfo.SelectUnitType.Random:
                    Unit _tUnit = GetUnitRandom();
                    if (_tUnit == null)
                        return;

                    for(int j =0;j< _curAliveUnitList.Count; j++)
                    {
                        if(_curAliveUnitList[j]._tr.Equals(_tUnit._tr))
                        {
                            _curAliveUnitList.RemoveAt(j);
                            break;
                        }
                    }

                   // _alrealySelectedUnitList.Add(_tUnit);
                    _nextBehaviour.DoBehaviour(_tUnit);
                    break;
            }
        }
       
    }

    public override void DoBehaviour(Unit _tUnit)
    {
        SetAliveUnit();
        switch (_getUnitType)
        {
            case EnumInfo.SelectUnitType.Random:

                Unit _tarUnit = GetUnitRandom();

                if (_tarUnit == null)
                    return;
                _nextBehaviour.DoBehaviour(_tarUnit);
                break;
        }
    }

    List<Unit> _curAliveUnitList = new List<Unit>();

    void SetAliveUnit()
    {
        if (_targetTeam.Equals(EnumInfo.TargetTeam.SameTeam))
            _curAliveUnitList = UnitManager.Instance.GetAliveUnitList(_unit._teamType);
        else if (_targetTeam.Equals(EnumInfo.TargetTeam.OppositeTeam))
            _curAliveUnitList = UnitManager.Instance.GetAliveUnitList(_unit._teamType.Equals(EnumInfo.TeamType.Player) ? EnumInfo.TeamType.Opposite : EnumInfo.TeamType.Player);
        else
            _curAliveUnitList = UnitManager.Instance._curAliveUnitsOnTile;

        if (!_includeSelf)
        {
            for (int i = 0; i < _curAliveUnitList.Count; i++)
                if (_curAliveUnitList[i]._tr.Equals(_unit._tr))
                { 
                    _curAliveUnitList.RemoveAt(i);
                    break;
                }
        }

    }

    Unit GetUnitRandom()
    {
        Unit _tUnit = _curAliveUnitList[Random.Range(0, _curAliveUnitList.Count)];



       
        return _tUnit;
    }
}
