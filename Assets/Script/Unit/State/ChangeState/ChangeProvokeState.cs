using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeProvokeState : ChangeState
{

    public override void InitChangeState(Unit _u)
    {
        base.InitChangeState(_u);
        _changeState = EnumInfo.State.Provoke;
    }
    public float duration;
}
