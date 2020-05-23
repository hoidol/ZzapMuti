using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAttakSpeedState : ChangeState
{

    public override void InitChangeState(Unit _u)
    {
        base.InitChangeState(_u);
        _changeState = EnumInfo.State.AttakSpeed;
    }
    public float _duration;
    public float _multiAttackSpeed;
}
