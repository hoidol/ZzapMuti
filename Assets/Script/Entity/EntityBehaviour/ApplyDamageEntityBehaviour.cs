using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamageEntityBehaviour : EntityBehaviour
{
    public override void DoBehaviour(Unit _tUnit)
    {
        _tUnit.stateMgr.TakeDamage(entity.ownUnit.unitRealData.normalDamage);
    }
}
