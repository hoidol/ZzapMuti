using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySkillDamageBehaviour : CommonBehaviour
{
    public override void DoBehaviour()
    {
        Debug.Log("ApplyDamageBehaviour - DoBehaviour()");
    }
    public override void DoBehaviour(Unit _tUnit)
    {
        Debug.Log("ApplyDamageBehaviour - DoBehaviour(_tUnit)");
        _tUnit.stateMgr.TakeDamage(_unit.unitRealData.skillDamage);
    }

}
