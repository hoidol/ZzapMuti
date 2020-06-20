using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedState : State
{
    [SerializeField] List<ChangeMoveSpeedChangeState> _moveSpeedChangeStateInfoList = new List<ChangeMoveSpeedChangeState>();
    [SerializeField] float _curMultiMoveSpeed;

    public override void InitState(Unit _u)
    {
        base.InitState(_u);
        _state = EnumInfo.State.MoveSpeed;
        _curMultiMoveSpeed = 1;
    }
    public override void StartBattle()
    {
        _moveSpeedChangeStateInfoList.Clear();
    }

    public override void ChangeState(ChangeState _cS)
    {
        Debug.Log("MoveSpeed! ChangeState");
        StartCoroutine(ProcessChangeState((ChangeMoveSpeedChangeState)_cS));
    }

    IEnumerator ProcessChangeState(ChangeMoveSpeedChangeState _cMSCState)
    {
        _moveSpeedChangeStateInfoList.Add(_cMSCState);
        CheckMultiMoveSpeed();
        yield return new WaitForSeconds(_cMSCState._duration);
        _moveSpeedChangeStateInfoList.Remove(_cMSCState);
        CheckMultiMoveSpeed();
    }


    void CheckMultiMoveSpeed()
    {
        _curMultiMoveSpeed = 1;
        for(int i =0;i< _moveSpeedChangeStateInfoList.Count; i++)
        {
            _curMultiMoveSpeed *= _moveSpeedChangeStateInfoList[i]._multiMoveSpeed;
        }
    }

    public float GetMoveSpeed()
    {
        return _curMultiMoveSpeed;
    }
}
