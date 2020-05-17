using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeRateState : State
{
    List<ChangeDodgeRateState> _dodgeRateStateInfoList = new List<ChangeDodgeRateState>();
    [SerializeField] float _curMultiDodgeRate;
    public override void InitState(Unit _u)
    {
        base.InitState(_u);
        _state = EnumInfo.State.DodgeRate;
    }

    public override void StartBattle()
    {
        _curMultiDodgeRate = 1;
        _dodgeRateStateInfoList.Clear();
    }
    public override void ChangeState(ChangeState _cS)
    {
        StartCoroutine(ProcessChangeState((ChangeDodgeRateState)_cS));
    }

    IEnumerator ProcessChangeState(ChangeDodgeRateState _cS)
    {
       

        _dodgeRateStateInfoList.Add(_cS);
        CheckDodgeReate();
        yield return new WaitForSeconds(_cS._duration);
        _dodgeRateStateInfoList.Remove(_cS);
        CheckDodgeReate();
    }


    void CheckDodgeReate()
    {
        _curMultiDodgeRate = 1;
        for (int i = 0; i < _dodgeRateStateInfoList.Count; i++)
        {
            _curMultiDodgeRate *= _dodgeRateStateInfoList[i]._dodgeRate;
        }
    }

    public float GetDodgeRate()
    {
        return _curMultiDodgeRate;
    }
}
