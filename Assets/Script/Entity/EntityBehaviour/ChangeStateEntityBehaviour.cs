using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateEntityBehaviour : EntityBehaviour
{

    [SerializeField] ChangeState _changeState;

    public override void InitEntityBehaviour(Entity _e)
    {
        base.InitEntityBehaviour(_e);
        _changeState = GetComponentInChildren<ChangeState>();
        _changeState.InitChangeState(_e.ownUnit);
    }

    public override void DoBehaviour(Unit _tUnit)
    {
        _tUnit.stateMgr.ChangeState(_changeState);
    }
}
