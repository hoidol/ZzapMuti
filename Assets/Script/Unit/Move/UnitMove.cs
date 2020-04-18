using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    Unit _curUnit;
    Unit _targetUnit;

    public void InitUnitMove(Unit _u)
    {
        _curUnit = _u;
    }
    public virtual void StartBattle()
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

            if (!_curUnit._needToMove)
                continue;

            
            _curUnit._tr.position = Vector2.MoveTowards(_curUnit._tr.position, _targetUnit._tr.position, Time.deltaTime * 2);
        }
    }




}
