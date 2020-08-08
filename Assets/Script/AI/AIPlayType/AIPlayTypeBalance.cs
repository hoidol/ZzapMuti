using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayTypeBalance : AIPlayType
{
    public override void InitAIPlayType()
    {
        _aiPlayType = EnumInfo.AIPlayType.Balance;
    }

    public override void StartTurn(int _r)
    {
        base.StartTurn(_r);

        AnalyzeSituation();


        // 상황 분석
        _gapUnitFeatureInfo.Sort(delegate (UnitFeatureInfo a, UnitFeatureInfo b)
        {
            if (a.Value > b.Value)
                return -1;
            if (a.Value < b.Value)
                return 1;
            return 0;
        });



    }
}
