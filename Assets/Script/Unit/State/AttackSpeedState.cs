using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedState : State
{
    [SerializeField] List<AttackSpeedStateInfo> _attackSpeedStateInfoList = new List<AttackSpeedStateInfo>();
    [SerializeField] float _curMultiAttackSpeed;
    public override void StartBattle()
    {
        _curMultiAttackSpeed = 1;
        _attackSpeedStateInfoList.Clear();
    }

    public override void ChangeState(ChangeState _cS)
    {
        StartCoroutine(ProcessChangeState((ChangeAttakSpeedState)_cS));
    }

    IEnumerator ProcessChangeState(ChangeAttakSpeedState _cMSCState)
    {
        AttackSpeedStateInfo _info = new AttackSpeedStateInfo();

        _info._duration = _cMSCState._duration;
        _info._multiAttackSpeed = _cMSCState._multiAttackSpeed;

        _attackSpeedStateInfoList.Add(_info);
        CheckMultiAttackSpeed();
        yield return new WaitForSeconds(_cMSCState._duration);
        _attackSpeedStateInfoList.Remove(_info);
        CheckMultiAttackSpeed();
    }


    void CheckMultiAttackSpeed()
    {
        _curMultiAttackSpeed = 1;
        for (int i = 0; i < _attackSpeedStateInfoList.Count; i++)
        {
            _curMultiAttackSpeed *= _attackSpeedStateInfoList[i]._multiAttackSpeed; //
        }
    }

    public float GetAttackSpeed() 
    {
        return _curMultiAttackSpeed; //원래 스피트 *  2 => 1/2  3=> 1/3  이면
    }
}


public class AttackSpeedStateInfo
{
    public float _duration;
    public float _multiAttackSpeed;
}