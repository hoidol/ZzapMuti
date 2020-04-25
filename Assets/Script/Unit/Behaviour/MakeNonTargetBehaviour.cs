using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNonTargetBehaviour : UnitBehaviour
{
    public UnitBehaviour _nextBehaviour;
    public override void InitUnitBehaviour(Unit _u)
    {
        base.InitUnitBehaviour(_u);

        _nextBehaviour.InitUnitBehaviour(_u);
    }

    public override void DoBehaviour()
    {
        _nextBehaviour.DoBehaviour();
    }

    public override void DoBehaviour(Unit _tUnit)
    {
        _nextBehaviour.DoBehaviour();
    }
}
