using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBehaviour : UnitBehaviour
{
    public string _effectIdx;

    public Effect _effect;

    public override void InitUnitBehaviour(Unit _u)
    {
        base.InitUnitBehaviour(_u);
        _effect.InitEffect();
    }
    public override void DoBehaviour()
    {
        if (_effect)
            _effect.PlayEffect();
    }

    public override void DoBehaviour(Unit _tUnit)
    {
        if (_effect == null)
            EffectManager.Instance.PlayEffect(_effectIdx, _tUnit._tr.position);
        else
            _effect.PlayEffect();

    }
}
