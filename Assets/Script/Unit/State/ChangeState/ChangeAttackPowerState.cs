using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAttackPowerState : ChangeState
{
    public override void InitChangeState(Unit _u)
    {
        base.InitChangeState(_u);
        _changeState = EnumInfo.State.AttackPower;
    }
    public float _duration;
    public float _multiAttackPower;
}
