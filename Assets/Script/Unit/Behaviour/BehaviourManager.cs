using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourManager : MonoBehaviour
{
    public Unit unit;

    ReinforceBehaviour[] reinforceBehaivours;    
    
    AttackSpeedState _attackSpeedState;
    [SerializeField] bool _ableToCallNormalBehaviour = false;
    public void InitBehaviourMgr(Unit _u)
    {
        unit = _u;

        reinforceBehaivours = GetComponentsInChildren<ReinforceBehaviour>();

        _attackSpeedState = (AttackSpeedState)unit._stateMgr.GetState(EnumInfo.State.AttakSpeed);
    }

    public void StartBattle()
    {
        _ableToCallNormalBehaviour = false;

        for(int i =0;i< reinforceBehaivours.Length; i++)
        {
            reinforceBehaivours[i].StartBattle();
        }

        Invoke("ResetCoolTime", unit.unitRealData.AttackSpeed * 1 / _attackSpeedState.GetAttackSpeed());
        StartCoroutine(ProcessNonTargetBehaviour());
    }

    public void TakedDameage()
    {
        //맞았을때
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
            Invoke("ResetCoolTime", unit.unitRealData.AttackSpeed * 1 / _attackSpeedState.GetAttackSpeed());

        }

        if (!_usedSkill && _ableToCallNormalBehaviour)
        {
            _ableToCallNormalBehaviour = false;
            CancelInvoke("ResetCoolTime");
            Invoke("ResetCoolTime", unit.unitRealData.AttackSpeed * 1 / _attackSpeedState.GetAttackSpeed());

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
