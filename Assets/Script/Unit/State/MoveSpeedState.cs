using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedState : State
{
    List<MoveSpeedStateInfo> _moveSpeedStateInfoList = new List<MoveSpeedStateInfo>();
    [SerializeField] float _curMultiMoveSpeed;

    public override void InitState(Unit _u)
    {
        base.InitState(_u);
        _state = EnumInfo.State.MoveSpeed;
    }
    public override void StartBattle()
    {
        _moveSpeedStateInfoList.Clear();
    }

    public override void ChangeState(ChangeState _cS)
    {
        StartCoroutine(ProcessChangeState((ChangeMoveSpeedChangeState)_cS));
    }

    IEnumerator ProcessChangeState(ChangeMoveSpeedChangeState _cMSCState)
    {
        MoveSpeedStateInfo _info = new MoveSpeedStateInfo();

        _info._duration = _cMSCState._duration;
        _info._multiMoveSpeed = _cMSCState._multiMoveSpeed;

        _moveSpeedStateInfoList.Add(_info);
        CheckMultiMoveSpeed();
        yield return new WaitForSeconds(_cMSCState._duration);
        _moveSpeedStateInfoList.Remove(_info);
        CheckMultiMoveSpeed();
    }


    void CheckMultiMoveSpeed()
    {
        _curMultiMoveSpeed = 1;
        for(int i =0;i< _moveSpeedStateInfoList.Count; i++)
        {
            _curMultiMoveSpeed *= _moveSpeedStateInfoList[i]._multiMoveSpeed;
        }
    }

    public float GetMoveSpeed()
    {
        return _curMultiMoveSpeed;
    }
}

public class MoveSpeedStateInfo
{
    public float _duration;
    public float _multiMoveSpeed;
}