using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySkillDamageBehaviour : UnitBehaviour
{
    public override void DoBehaviour()
    {
        Debug.Log("ApplyDamageBehaviour - DoBehaviour()");
    }
    public override void DoBehaviour(Unit _tUnit)
    {
        Debug.Log("ApplyDamageBehaviour - DoBehaviour(_tUnit)");
        _tUnit._stateMgr.TakeDamage(_unit.unitRealData.skillDamage);
    }

}
