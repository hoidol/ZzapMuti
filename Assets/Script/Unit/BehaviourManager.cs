using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourManager : MonoBehaviour
{
    public Unit _unit;
    public UnitBehaviour _normalBehaviour; 
    public UnitBehaviour _skillBehaviour;
    
    public void InitBehaviourMgr(Unit _u)
    {
        _unit = _u;
        if (_normalBehaviour)
            _normalBehaviour.InitUnitBehaviour(_unit);
        if (_skillBehaviour)
            _skillBehaviour.InitUnitBehaviour(_unit);
    }

    public void StartBattle()
    {
        _ableToCallNormalBehaviour = false;


        if (_normalBehaviour)
            _normalBehaviour.StartBattle();

        if (_skillBehaviour)
            _skillBehaviour.StartBattle();

        StartCoroutine(CoolTime());
        StartCoroutine(ProcessNonTargetBehaviour());
    }


    IEnumerator ProcessNonTargetBehaviour()
    {
        while (true)
        {
            bool _usedSkill = false;
            if(_skillBehaviour != null)
            {
                if (_skillBehaviour._nonTarget)
                {
                    if (_unit._stateMgr._curMana >= _unit._unitData.MaxMana)
                    {
                       
                        _ableToCallNormalBehaviour = false;
                        StartCoroutine(CoolTime());

                        _skillBehaviour.DoBehaviour();                        
                    }
                    _usedSkill = true;
                }
            }
           
            if(_normalBehaviour != null)
            {

                if (!_usedSkill)
                {
                    if (_normalBehaviour._nonTarget && _ableToCallNormalBehaviour)
                    {
                        _ableToCallNormalBehaviour = false;
                        StartCoroutine(CoolTime());

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

    public void DoBehaviour(Unit _u)
    {
        bool _usedSkill = false;
        if (_skillBehaviour != null)
        {
            if (_unit._stateMgr._curMana >= _unit._unitData.MaxMana)
            {
                _ableToCallNormalBehaviour = false;
                _unit._stateMgr._curMana = 0;
                StartCoroutine(CoolTime());
                _skillBehaviour.DoBehaviour(_u);
                _usedSkill = true;
            }
        }

        if (_normalBehaviour != null)
        {
            if (!_usedSkill && _ableToCallNormalBehaviour)
            {
                _normalBehaviour.DoBehaviour(_u);
                _ableToCallNormalBehaviour = false;
                StartCoroutine(CoolTime());
            }
        }
            
    }

    bool _ableToCallNormalBehaviour = false;
    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(_unit._unitData.AttackSpeed);
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
