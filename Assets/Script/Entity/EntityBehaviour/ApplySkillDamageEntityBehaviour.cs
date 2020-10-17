using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySkillDamageEntityBehaviour : EntityBehaviour
{
    public override void DoBehaviour(Unit _tUnit)
    {
        
            _tUnit._stateMgr.TakeDamage(entity.ownUnit.unitRealData.skillDamage);
    }
}
