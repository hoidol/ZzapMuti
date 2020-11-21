using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonBehaviour : UnitBehaviour
{
    public override void InitUnitBehaviour(Unit _u)
    {
        base.InitUnitBehaviour(_u);
        rootBehaviour = false;
        type = EnumInfo.UnitBehaviourType.SKILL;
    }
}
