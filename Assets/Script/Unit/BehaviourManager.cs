using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourManager : MonoBehaviour
{
    public Unit _unit;
    public UnitBehaviour _normalBehaviour; 
    public UnitBehaviour _skillBehaviour;

    AttackSpeedState _attackSpeedState;
    [SerializeField] bool _ableToCallNormalBehaviour = false;
    public void InitBehaviourMgr(Unit _u)
    {
        _unit = _u;
        if (_normalBehaviour)
            _normalBehaviour.InitUnitBehaviour(_unit);
        if (_skillBehaviour)
            _skillBehaviour.InitUnitBehaviour(_unit);

        _attackSpeedState = (AttackSpeedState)_unit._stateMgr.GetState(EnumInfo.State.AttakSpeed);
    }

    public void StartBattle()
    {
        _ableToCallNormalBehaviour = false;

        if (_normalBehaviour)
            _normalBehaviour.StartBattle();

        if (_skillBehaviour)
            _skillBehaviour.StartBattle();

        Invoke("ResetCoolTime", _unit._unitData.AttackSpeed * 1 / _attackSpeedState.GetAttackSpeed());
        StartCoroutine(ProcessNonTargetBehaviour());
    }

    IEnumerator ProcessNonTargetBehaviour()
    {
        while (true)
        {
            bool _usedSkill = false;
            if(_skillBehaviour)
            {
                if (_skillBehaviour._nonTarget)
                {
                    if (_unit._stateMgr._curMana >= _unit._unitData.MaxMana)
                    {
                        _ableToCallNormalBehaviour = false;
                        _unit._stateMgr.ConsumeAllMana();
                        _skillBehaviour.DoBehaviour();
                        _usedSkill = true;
                        CancelInvoke("ResetCoolTime");
                        Invoke("ResetCoolTime", _unit._unitData.AttackSpeed * 1 / _attackSpeedState.GetAttackSpeed());
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
                        Invoke("ResetCoolTime", _unit._unitData.AttackSpeed * 1 / _attackSpeedState.GetAttackSpeed());

                        _normalBehaviour.DoBehaviour();
                    }
                }
            }
                
            yield return null;
        }
    }


    public void DoBehaviour()
    {

    }

    public void DoBehaviour(Unit _tU)
    {
        bool _usedSkill = false;
        if (_skillBehaviour)
        {
            if (!_skillBehaviour._nonTarget)
            {
                if (_unit._stateMgr._curMana >= _unit._unitData.MaxMana)
                {

                    _ableToCallNormalBehaviour = false;
                    _unit._stateMgr.ConsumeAllMana();
                    _skillBehaviour.DoBehaviour(_tU);
                    _usedSkill = true;
                    CancelInvoke("ResetCoolTime");
                    Invoke("ResetCoolTime", _unit._unitData.AttackSpeed * 1/_attackSpeedState.GetAttackSpeed());

                }
            }            
        }

        if (_normalBehaviour)
        {
            if (!_normalBehaviour._nonTarget)
            {
                if (!_usedSkill && _ableToCallNormalBehaviour)
                {
                    _ableToCallNormalBehaviour = false;
                    CancelInvoke("ResetCoolTime");
                    Invoke("ResetCoolTime", _unit._unitData.AttackSpeed * 1 / _attackSpeedState.GetAttackSpeed());
                    _normalBehaviour.DoBehaviour(_tU);
                }
            }
           
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
