﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRepeatDamageState : ChangeState
{
    public override void InitChangeState(Unit _u)
    {
        base.InitChangeState(_u);
        _changeState = EnumInfo.State.RepeatDamage;
    }
    public float _duration;
    public float _damage;
}