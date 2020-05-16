using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealLowestHp : UnitBehaviour
{
    List<Unit> _sameTeamUnitList = new List<Unit>();
    public float _healRate;
    public int _healCount;
    List<Unit> _healedUnitList = new List<Unit>();
    public override void InitUnitBehaviour(Unit _u)
    {
        base.InitUnitBehaviour(_u);
    }

    public override void StartBattle()
    {
        _sameTeamUnitList.Clear();
        for(int i =0;i< UnitManager.Instance._curUnitsOnTile.Count; i++)
        {
            if (_unit._teamType.Equals(UnitManager.Instance._curUnitsOnTile[i]._teamType))
                _sameTeamUnitList.Add(UnitManager.Instance._curUnitsOnTile[i]);
        }
    }
    public override void DoBehaviour()
    {
        // 가장 체력이 낮은 유닛 찾기
        _healedUnitList.Clear();

        for(int i =0;i< _healCount; i++)
        {
           Unit _tUnit = SearLowestHpUnit();
            if (_tUnit == null)
                return;

            //힐 이펙트
            _tUnit._stateMgr.HealHp(_tUnit._unitData.Hp * _healRate);
        }
    }

    Unit SearLowestHpUnit()
    {
        float _lowestRate = float.MaxValue;
        Unit _u = null;
        for(int i =0;i< _sameTeamUnitList.Count; i++)
        {
            bool alreadyHealed = false;
            for(int j =0;j< _healedUnitList.Count; j++)
            {
                if (_healedUnitList[j]._tr.Equals(_sameTeamUnitList[i]._tr))
                {
                    alreadyHealed = true;
                    break;                    
                }
            }
            if (alreadyHealed)
                continue;

            float _tempRate = _sameTeamUnitList[i]._stateMgr._curHp/_sameTeamUnitList[i]._unitData.Hp;
            if(_lowestRate > _tempRate)
            {
                _lowestRate = _tempRate;
                _u = _sameTeamUnitList[i];
            }
        }
        return _u;
    }
}
