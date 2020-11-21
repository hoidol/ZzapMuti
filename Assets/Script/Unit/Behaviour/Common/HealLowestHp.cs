using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealLowestHp : CommonBehaviour
{
   [SerializeField]  List<Unit> _sameTeamUnitList = new List<Unit>();
    public float _healRate;
    public int _healCount;
    [SerializeField]  List<Unit> _healedUnitList = new List<Unit>();

    public string _healEffectIdx;
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
        Debug.Log("HealLowestHp 호출!");
        for(int i =0;i< _healCount; i++)
        {
           Unit _tUnit = SearchLowestHpUnit();
            if (_tUnit == null)
                return;
            _healedUnitList.Add(_tUnit);
            //힐 이펙트
            Debug.Log("힐주자 " + _tUnit.name + " _tUnit._unitData.Hp * _healRate : " + _tUnit.unitRealData.Hp * _healRate); 
            _tUnit.stateMgr.HealHp(_tUnit.unitRealData.Hp * _healRate);
            EffectManager.Instance.PlayEffect(_healEffectIdx, _tUnit._tr.position);
        }
    }

    Unit SearchLowestHpUnit()
    {
        float _lowestRate = float.MaxValue;
        Unit _u = null;
        for(int i =0;i< _sameTeamUnitList.Count; i++)
        {
            if (!_sameTeamUnitList[i].gameObject.activeSelf)
                continue;

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

            float _tempRate = _sameTeamUnitList[i].stateMgr._curHp/_sameTeamUnitList[i].unitRealData.Hp;
            if(_lowestRate > _tempRate)
            {
                _lowestRate = _tempRate;
                _u = _sameTeamUnitList[i];
            }
        }
        return _u;
    }
}
