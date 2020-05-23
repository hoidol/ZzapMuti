using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDodgeRateState : ChangeState
{
    public override void InitChangeState(Unit _u)
    {
        base.InitChangeState(_u);
        _changeState = EnumInfo.State.DodgeRate;
    }

    public float _duration;
    public float _dodgeRate;
}
