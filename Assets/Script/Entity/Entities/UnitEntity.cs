using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEntity : Entity
{
    public int reinforce;

    public override void CallEntity(Unit _u, Unit _tUnit)
    {
        base.CallEntity(_u, _tUnit);

        reinforce = _u.unitData.ReinforceLv;
    }
}
