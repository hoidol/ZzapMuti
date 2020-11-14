using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourManager : MonoBehaviour
{
    public Unit unit;

    UnitBehaviour[] unitBehaivours;
    UnitBehaviour curUnitBehaivour;
    [SerializeField] bool _ableToCallNormalBehaviour = false;
    public void InitBehaviourMgr(Unit _u)
    {
        unit = _u;
        unitBehaivours = GetComponentsInChildren<UnitBehaviour>();       
    }

    public void SetUserData(UnitData _uData, EnumInfo.TeamType _tType)
    {
        curUnitBehaivour = unitBehaivours[_uData.ReinforceLv - 1];
        curUnitBehaivour.SetUserData(_uData, _tType);
    }

    public void StartBattle()
    {
        _ableToCallNormalBehaviour = false;
        curUnitBehaivour.StartBattle();
        
        Invoke("ResetCoolTime", unit.unitRealData.AttackSpeed * 1 / unit.unitRealData.AttackSpeed);
        StartCoroutine(ProcessNonTargetBehaviour());
    }

    public void StartBehaviour()
    {
        //NonTargetBehaviour있으면 돌리고 없으면 돌리지마
        curUnitBehaivour.StartBehaviour();
    }

    Unit targetUnit;
    public void SetTargetUnit(Unit _tUnit)
    {
        targetUnit = _tUnit;
    }

    IEnumerator ProcessNonTargetBehaviour()
    {
        while (true)
        {
            bool _usedSkill = false;
           /* if(_skillBehaviour)
            {
                if (_skillBehaviour._nonTarget)
                {
                    if (unit._stateMgr._curMana >= unit.unitRealData.MaxMana)
                    {
                        _ableToCallNormalBehaviour = false;
                        unit._stateMgr.ConsumeAllMana();
                        _skillBehaviour.DoBehaviour();
                        _usedSkill = true;
                        CancelInvoke("ResetCoolTime");
                        Invoke("ResetCoolTime", unit.unitRealData.AttackSpeed * 1 / _attackSpeedState.GetAttackSpeed());
                    }
                }
            }
           
            if(_normalBehaviour)
            {
                if (!_usedSkill)
                {
                    if (_normalBehaviour._nonTarget && _ableToCallNormalBehaviour)
                    {
                        _ableToCallNormalBehaviour = false;
                        CancelInvoke("ResetCoolTime");
                        Invoke("ResetCoolTime", unit.unitRealData.AttackSpeed * 1 / _attackSpeedState.GetAttackSpeed());

                        _normalBehaviour.DoBehaviour();
                    }
                }
            }
               */ 
            yield return null;
        }
    }


    public void DoBehaviour()
    {

    }

    public void DoBehaviour(Unit _tU)
    {
        bool _usedSkill = false;
        if (unit._stateMgr._curMana >= unit.unitRealData.MaxMana)
        {

            _ableToCallNormalBehaviour = false;
            unit._stateMgr.ConsumeAllMana();

            //마법 써라
           // _skillBehaviour.DoBehaviour(_tU);
            _usedSkill = true;
            CancelInvoke("ResetCoolTime");
            Invoke("ResetCoolTime", unit.unitRealData.AttackSpeed * 1 / unit.unitRealData.AttackSpeed);

        }

        if (!_usedSkill && _ableToCallNormalBehaviour)
        {
            _ableToCallNormalBehaviour = false;
            CancelInvoke("ResetCoolTime");
            Invoke("ResetCoolTime", unit.unitRealData.AttackSpeed * 1 / unit.unitRealData.AttackSpeed);

            //일반 공격해라
            //_normalBehaviour.DoBehaviour(_tU);
        }
    }

    void ResetCoolTime()
    {
        _ableToCallNormalBehaviour = true;
    }


    public void Die()
    {
        StopAllCoroutines();
    }




    public void FinishBattle()
    {

    }
}
