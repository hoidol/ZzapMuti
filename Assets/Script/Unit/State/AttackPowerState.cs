using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPowerState : State
{
    List<ChangeAttackPowerState> _attackPowerChangeStateInfoList = new List<ChangeAttackPowerState>();
    [SerializeField] float _curMultiAttackPower;

    public override void InitState(Unit _u)
    {
        base.InitState(_u);
        _state = EnumInfo.State.AttackPower;
    }
    public override void StartBattle()
    {
        _attackPowerChangeStateInfoList.Clear();
    }

    public override void ChangeState(ChangeState _aS)
    {
        StartCoroutine(ProcessChangeState((ChangeAttackPowerState)_aS));
    }

    IEnumerator ProcessChangeState(ChangeAttackPowerState _aS)
    {
        _attackPowerChangeStateInfoList.Add(_aS);
        CheckAttackPower();
        yield return new WaitForSeconds(_aS._duration);
        _attackPowerChangeStateInfoList.Remove(_aS);
        CheckAttackPower();
    }


    void CheckAttackPower()
    {
        _curMultiAttackPower = 1;
        for (int i = 0; i < _attackPowerChangeStateInfoList.Count; i++)
        {
            _curMultiAttackPower *= _attackPowerChangeStateInfoList[i]._multiAttackPower;
        }

        _unit._normalDamage.DamagePower = _unit._unitData.Damage + (_unit._unitData.Damage * _curMultiAttackPower);
        _unit._skillDamage.DamagePower = _unit._unitData.SkillDamage + (_unit._unitData.SkillDamage * _curMultiAttackPower);
        
    
    }

    public float GetAttackPower()
    {
        return _curMultiAttackPower;
    }
}
