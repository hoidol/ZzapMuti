using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : Unit
{
    public override IEnumerator ProcessSearch() //적 찾고 이동 관련 
    {
        while (true)
        {
            if (provokeState._curProvokeStateInfo._resourceUnit != null)
            {
                if (provokeState._curProvokeStateInfo._resourceUnit.stateMgr._isLiving)
                {
                    ProcessBehaviour(provokeState._curProvokeStateInfo._resourceUnit);
                    continue;
                }
            }

            if (targetUnit != null)
            {
                if (!targetUnit.stateMgr._isLiving)
                    targetUnit = UnitManager.Instance.SearchNearestEnemyUnit(this);
            }
            else
            {
                targetUnit = UnitManager.Instance.SearchNearestEnemyUnit(this);
            }

            if (targetUnit == null)
                continue;

            ProcessBehaviour(targetUnit);

            yield return new WaitForSeconds(0.1f);
        }
    }

    void ProcessBehaviour(Unit _tUnit)
    {
        animMgr.UpdateDirection(_tUnit._tr.position - _tr.position);
        if (unitRealData.AttackDistance >= Vector2.Distance(_tr.position, _tUnit._tr.position)) // 공격 가능 상태
        {
            needToMove = false;

            if (ableToAttack) //공격 속도로
            {
                ableToAttack = false;
                StartCoroutine(ProcessCoolTime());
                if (stateMgr.CheckFullMana())
                    behaviourMgr.curBehaviourContainer.SkillBehaviour(targetUnit);
                else
                    behaviourMgr.curBehaviourContainer.NormalBehaviour(targetUnit);
            }

        }
        else
        {
            needToMove = true;
            moveMgr.MoveToUnit(_tUnit);
        }
    }

    public override IEnumerator ProcessCoolTime()
    {
        yield return new WaitForSeconds(unitRealData.AttackSpeed);
        ableToAttack = true;
    }
}
