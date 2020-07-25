using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayTypeAttack : AIPlayType
{
    public override void InitAIPlayType()
    {
        _aiPlayType = EnumInfo.AIPlayType.Attack;
    }
}
