using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamageEntityBehaviour : EntityBehaviour
{
    public bool _isSkillDamage;
    public override void DoBehaviour(Unit _tUnit)
    {
        if (_isSkillDamage)
        {
            _tUnit._stateMgr.TakeDamage(entity.ownUnit._skillDamage);
        }
        else
        {
            _tUnit._stateMgr.TakeDamage(entity.ownUnit._normalDamage);
        }
    }
}
