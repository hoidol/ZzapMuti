using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamageBehaviour : UnitBehaviour
{
    public override void DoBehaviour()
    {
    }
    public override void DoBehaviour(Unit _tUnit)
    {
        _tUnit._stateMgr.TakeDamage(_unit._unitStatData._normalDamage);
    }
}
