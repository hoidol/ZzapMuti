using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUnitOnTileBehaviour : UnitBehaviour
{
    public EnumInfo.GetUnitType _getUnitType;
    public EnumInfo.TargetTeam _targetTeam;
    public UnitBehaviour _nextBehaviour;
    public bool _includeSelf;

    public override void InitUnitBehaviour(Unit _u)
    {
        base.InitUnitBehaviour(_u);
        if (_nextBehaviour)
            _nextBehaviour.InitUnitBehaviour(_u);
    }

    public override void DoBehaviour()
    {
        switch (_getUnitType)
        {
            case EnumInfo.GetUnitType.Random:
                Unit _tUnit = GetUnitRandom();

                if (_tUnit == null)
                    return;
                _nextBehaviour.DoBehaviour(_tUnit);
                break;
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

   





    Unit GetUnitRandom()
    {        
        switch (_targetTeam)
        {
            case EnumInfo.TargetTeam.Both:

                if (!_includeSelf)
                {
                    if (UnitManager.Instance._curAliveUnitsOnTile.Count <= 1)
                        return null;

                    Unit _tUnit = UnitManager.Instance._curAliveUnitsOnTile[Random.Range(0, UnitManager.Instance._curAliveUnitsOnTile.Count)];
                    while (_tUnit._tr.Equals(_unit._tr))
                    {
                        _tUnit = UnitManager.Instance._curAliveUnitsOnTile[Random.Range(0, UnitManager.Instance._curAliveUnitsOnTile.Count)];
                    }
                    return _tUnit;
                }
                else
                {
                    return UnitManager.Instance._curAliveUnitsOnTile[Random.Range(0, UnitManager.Instance._curAliveUnitsOnTile.Count)];
                   
                }


            case EnumInfo.TargetTeam.SameTeam:

                if (_unit._teamType.Equals(EnumInfo.TeamType.Red))
                {
                    if (!_includeSelf)
                    {
                        if (UnitManager.Instance._curAliveRedUnitsOnTile.Count <= 1)
                            return null;

                        Unit _tUnit = UnitManager.Instance._curAliveRedUnitsOnTile[Random.Range(0, UnitManager.Instance._curAliveRedUnitsOnTile.Count)];
                        while (_tUnit._tr.Equals(_unit._tr))
                            _tUnit = UnitManager.Instance._curAliveRedUnitsOnTile[Random.Range(0, UnitManager.Instance._curAliveRedUnitsOnTile.Count)];

                        return _tUnit;

                    }
                    return UnitManager.Instance._curAliveRedUnitsOnTile[Random.Range(0, UnitManager.Instance._curAliveRedUnitsOnTile.Count)];
                }
                else
                {
                    if (!_includeSelf)
                    {
                        if (UnitManager.Instance._curAliveBlueUnitsOnTile.Count <= 1)
                            return null;

                        Unit _tUnit = UnitManager.Instance._curAliveBlueUnitsOnTile[Random.Range(0, UnitManager.Instance._curAliveBlueUnitsOnTile.Count)];
                        while (_tUnit._tr.Equals(_unit._tr))
                            _tUnit = UnitManager.Instance._curAliveBlueUnitsOnTile[Random.Range(0, UnitManager.Instance._curAliveBlueUnitsOnTile.Count)];

                        return _tUnit;
                    }
                    return UnitManager.Instance._curAliveBlueUnitsOnTile[Random.Range(0, UnitManager.Instance._curAliveBlueUnitsOnTile.Count)];
                }

            case EnumInfo.TargetTeam.OppositeTeam:

                if (_unit._teamType.Equals(EnumInfo.TeamType.Red))
                {
                    if (UnitManager.Instance._curAliveBlueUnitsOnTile.Count <= 0)
                        return null;
                    return UnitManager.Instance._curAliveBlueUnitsOnTile[Random.Range(0, UnitManager.Instance._curAliveBlueUnitsOnTile.Count)];
                }
                else
                {
                    if (UnitManager.Instance._curAliveRedUnitsOnTile.Count <= 0)
                        return null;
                    return UnitManager.Instance._curAliveRedUnitsOnTile[Random.Range(0, UnitManager.Instance._curAliveRedUnitsOnTile.Count)];
                }
        }
        return null;
    }
}
