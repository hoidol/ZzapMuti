using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayTypeReinforce : AIPlayType
{
    public override void InitAIPlayType()
    {
        _aiPlayType = EnumInfo.AIPlayType.Reinforce;
    }
}
