using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayTypeCC : AIPlayType
{
    public override void InitAIPlayType()
    {
        _aiPlayType = EnumInfo.AIPlayType.CC;
    }
}
