using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateBehaviour : UnitBehaviour
{
    [SerializeField] ChangeState _changeState;

    public override void InitUnitBehaviour(Unit _u)
    {
        base.InitUnitBehaviour(_u);
        _changeState = GetComponentInChildren<ChangeState>();
        _changeState.InitChangeState(_u);
    }
    public override void DoBehaviour(Unit _tUnit)
    {
        _tUnit._stateMgr.ChangeState(_changeState);
    }
}
