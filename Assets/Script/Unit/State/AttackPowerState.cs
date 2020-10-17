using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPowerState : State
{
    List<ChangeAttackPowerState> _attackPowerChangeStateInfoList = new List<ChangeAttackPowerState>();
    [SerializeField] float curMultiAttackPower;

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
        curMultiAttackPower = 1;
        for (int i = 0; i < _attackPowerChangeStateInfoList.Count; i++)
        {
            curMultiAttackPower *= _attackPowerChangeStateInfoList[i]._multiAttackPower;
        }

        _unit.unitRealData.normalDamage.DamagePower = _unit.unitRealData.normalDamage.DamagePower + (_unit.unitRealData.normalDamage.DamagePower * curMultiAttackPower);
        _unit.unitRealData.skillDamage.DamagePower = _unit.unitRealData.skillDamage.DamagePower + (_unit.unitRealData.skillDamage.DamagePower * curMultiAttackPower);
        
    
    }

    public float GetAttackPower()
    {
        return curMultiAttackPower;
    }
}
