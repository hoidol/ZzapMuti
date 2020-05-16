using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUnitOnTileBehaviour : UnitBehaviour
{
    public EnumInfo.GetUnitType _getUnitType;
    public EnumInfo.TargetTeam _targetTeam;
    public UnitBehaviour _nextBehaviour;
    public bool _includeSelf;
    public int _count;
    List<Unit> _alrealyGetUnitList = new List<Unit>();
    public override void InitUnitBehaviour(Unit _u)
    {
        base.InitUnitBehaviour(_u);
        if (_nextBehaviour)
            _nextBehaviour.InitUnitBehaviour(_u);
    }

    public override void DoBehaviour()
    {
        for(int i =0;i< _count; i++)
        {
            switch (_getUnitType)
            {
                case EnumInfo.GetUnitType.Random:
                    SetAliveUnit();
                    Unit _tUnit = GetUnitRandom();

                    if (_tUnit == null)
                        return;

                    _alrealyGetUnitList.Add(_tUnit);
                    _nextBehaviour.DoBehaviour(_tUnit);
                    break;
            }
        }
       
    }

    public override void DoBehaviour(Unit _tUnit)
    {
        switch (_getUnitType)
        {
            case EnumInfo.GetUnitType.Random:
                Unit _tarUnit = GetUnitRandom();

                if (_tarUnit == null)
                    return;
                _nextBehaviour.DoBehaviour(_tarUnit);
                break;
        }
    }

    List<Unit> _curAliveUnitsOnTile = new List<Unit>();
    List<Unit> _curAliveRedUnitsOnTile = new List<Unit>();
    List<Unit> _curAliveBlueUnitsOnTile = new List<Unit>();

    void SetAliveUnit()
    {
        for(int i =0;i< UnitManager.Instance._curAliveUnitsOnTile.Count; i++)
        {
            bool _overlap = false;
            for(int j =0;j< _alrealyGetUnitList.Count; j++)
            {
                if (UnitManager.Instance._curAliveUnitsOnTile[i]._tr.Equals(_alrealyGetUnitList[j]._tr))
                {
                    _overlap = true;
                    break;
                }
            }
            if (_overlap)
                continue;

            switch (UnitManager.Instance._curAliveUnitsOnTile[i]._teamType)
            {
                case EnumInfo.TeamType.Red:
                    _curAliveRedUnitsOnTile.Add(UnitManager.Instance._curAliveUnitsOnTile[i]);
                    break;

                case EnumInfo.TeamType.Blue:
                    _curAliveBlueUnitsOnTile.Add(UnitManager.Instance._curAliveUnitsOnTile[i]);
                    break;
            }

            _curAliveUnitsOnTile.Add(UnitManager.Instance._curAliveUnitsOnTile[i]);
        }
    }

    Unit GetUnitRandom()
    {        
        switch (_targetTeam)
        {
            case EnumInfo.TargetTeam.Both:

                if (!_includeSelf)
                {
                    if (_curAliveUnitsOnTile.Count <= 1)
                        return null;

                    Unit _tUnit = _curAliveUnitsOnTile[Random.Range(0, _curAliveUnitsOnTile.Count)];
                    while (_tUnit._tr.Equals(_unit._tr))
                    {
                        _tUnit = _curAliveUnitsOnTile[Random.Range(0, _curAliveUnitsOnTile.Count)];
                    }
                    return _tUnit;
                }
                else
                {
                    return _curAliveUnitsOnTile[Random.Range(0,_curAliveUnitsOnTile.Count)];
                   
                }


            case EnumInfo.TargetTeam.SameTeam:

                if (_unit._teamType.Equals(EnumInfo.TeamType.Red))
                {
                    if (!_includeSelf)
                    {
                        if (_curAliveRedUnitsOnTile.Count <= 1)
                            return null;

                        Unit _tUnit = _curAliveRedUnitsOnTile[Random.Range(0, _curAliveRedUnitsOnTile.Count)];
                        while (_tUnit._tr.Equals(_unit._tr))
                            _tUnit = _curAliveRedUnitsOnTile[Random.Range(0, _curAliveRedUnitsOnTile.Count)];

                        return _tUnit;

                    }
                    return _curAliveRedUnitsOnTile[Random.Range(0,_curAliveRedUnitsOnTile.Count)];
                }
                else
                {
                    if (!_includeSelf)
                    {
                        if (_curAliveBlueUnitsOnTile.Count <= 1)
                            return null;

                        Unit _tUnit = _curAliveBlueUnitsOnTile[Random.Range(0, _curAliveBlueUnitsOnTile.Count)];
                        while (_tUnit._tr.Equals(_unit._tr))
                            _tUnit = _curAliveBlueUnitsOnTile[Random.Range(0, _curAliveBlueUnitsOnTile.Count)];

                        return _tUnit;
                    }
                    return _curAliveBlueUnitsOnTile[Random.Range(0, _curAliveBlueUnitsOnTile.Count)];
                }

            case EnumInfo.TargetTeam.OppositeTeam:

                if (_unit._teamType.Equals(EnumInfo.TeamType.Red))
                {
                    if (_curAliveBlueUnitsOnTile.Count <= 0)
                        return null;
                    return _curAliveBlueUnitsOnTile[Random.Range(0, _curAliveBlueUnitsOnTile.Count)];
                }
                else
                {
                    if (_curAliveRedUnitsOnTile.Count <= 0)
                        return null;
                    return _curAliveRedUnitsOnTile[Random.Range(0, _curAliveRedUnitsOnTile.Count)];
                }
        }
        return null;
    }
}
