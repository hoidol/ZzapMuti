using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMoveSpeedChangeState : ChangeState
{
    public override void InitChangeState(Unit _u)
    {
        base.InitChangeState(_u);
        _changeState = EnumInfo.State.MoveSpeed;
    }
    public float _duration;
    public float _multiMoveSpeed;
    public string _effect;


}
