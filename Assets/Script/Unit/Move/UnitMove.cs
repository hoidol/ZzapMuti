using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    protected Unit _unit;
    protected Unit _targetUnit;

    protected MoveSpeedState _moveSpeedState;

    public void InitUnitMove(Unit _u)
    {
        _unit = _u;
        _moveSpeedState = (MoveSpeedState)_unit._stateMgr.GetState(EnumInfo.State.MoveSpeed);
    }
    public virtual void StartBattle()
    {
        
    }

    public virtual void SetPosition()
    {
        StartCoroutine(ProcessMove());
    }

    public virtual void StopMove()
    {
    }


    public virtual void MoveToUnit(Unit _tU)
    {
        _targetUnit = _tU;
    }

    IEnumerator ProcessMove()
    {
        while (true)
        {
            yield return null;

            if (!_unit._needToMove)
                continue;

            _unit._tr.position = Vector2.MoveTowards(_unit._tr.position, _targetUnit._tr.position, Time.deltaTime * _moveSpeedState.GetMoveSpeed());
        }
    }


    public virtual void RestorePosition()
    {

    }
    public virtual void FinishBattle()
    {
        StopAllCoroutines();
    }

}
