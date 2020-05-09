using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireState : State
{
    public override void InitState(Unit _u)
    {
        base.InitState(_u);
        _state = EnumInfo.State.Fire;
    }

    public override void StartBattle()
    {
    }

    public override void ChangeState(ChangeState _cS)
    {
    }



}

public class FireStateInfo{
    }
