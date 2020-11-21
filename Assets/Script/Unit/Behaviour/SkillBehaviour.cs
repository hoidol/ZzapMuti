using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBehaviour : UnitBehaviour
{
    public UnitBehaviour nextBehaviour;
    public override void InitUnitBehaviour(Unit _u)
    {
        base.InitUnitBehaviour(_u);
        rootBehaviour = true;
        type = EnumInfo.UnitBehaviourType.SKILL;
    }
    public override void DoBehaviour(Unit _tUnit = null)
    {
        nextBehaviour.DoBehaviour(_unit);
    }

}
