using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Unit
{
    public override IEnumerator ProcessBehaviour()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (provokeState._curProvokeStateInfo._resourceUnit != null)
            {
                if (provokeState._curProvokeStateInfo._resourceUnit._stateMgr._isLiving)
                {
                    ProcessMoveAndAttack(provokeState._curProvokeStateInfo._resourceUnit);
                    continue;
                }
            }

            if(_targetUnit != null)
            {
                if(!_targetUnit._stateMgr._isLiving)
                    _targetUnit = UnitManager.Instance.SearchNearestEnemyUnit(this);
            }
            else
            {
                _targetUnit = UnitManager.Instance.SearchNearestEnemyUnit(this);
            }

            if (_targetUnit == null)
                continue;

            ProcessMoveAndAttack(_targetUnit);
        }       
    }

    void ProcessMoveAndAttack(Unit _tUnit)
    {
        _animMgr.UpdateDirection(_tUnit._tr.position - _tr.position);

        if (_unitData.AttackDistance >= Vector2.Distance(_tr.position, _tUnit._tr.position)) // 공격 가능 상태
        {
            _ableToAttack = true;
            _needToMove = false;

            _behaviourMgr.DoBehaviour(_tUnit);
        }
        else
        {
            _ableToAttack = false;
            _needToMove = true;
            _moveMgr.MoveToUnit(_tUnit);
        }
    }




}
