using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayTypeDefence : AIPlayType
{
    public override void InitAIPlayType()
    {
        _aiPlayType = EnumInfo.AIPlayType.Defence;
    }
}
