using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFireState : ChangeState
{
    public float _duration;
    public Damage _damage;

    public override void InitChangeState(Unit _u)
    {
        base.InitChangeState(_u);
        _damage.ResourceUnit = _u;
        _changeState = EnumInfo.State.Fire;
    }
}
