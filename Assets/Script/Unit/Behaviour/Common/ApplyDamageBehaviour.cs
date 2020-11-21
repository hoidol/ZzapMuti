using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamageBehaviour : CommonBehaviour
{
    public override void DoBehaviour()
    {
    }
    public override void DoBehaviour(Unit _tUnit)
    {
        _tUnit.stateMgr.TakeDamage(_unit.unitRealData.normalDamage);
    }
}
