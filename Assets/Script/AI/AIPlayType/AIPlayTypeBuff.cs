using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayTypeBuff : AIPlayType
{
    public override void InitAIPlayType()
    {
        _aiPlayType = EnumInfo.AIPlayType.Buff;
    }
}
