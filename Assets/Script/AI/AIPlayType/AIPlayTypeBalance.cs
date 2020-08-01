using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayTypeBalance : AIPlayType
{
    public override void InitAIPlayType()
    {
        base.InitAIPlayType();
        _aiPlayType = EnumInfo.AIPlayType.Balance;
    }

    public override void StartTurn(int _r)
    {
        base.StartTurn(_r);

        if (_selectedUnitData.Count <= 0)
            return;
        if (_r <= 1)
        {
            UnitData _uData = _selectedUnitData[Random.Range(0, _selectedUnitData.Count)];
            UnitManager.Instance.CreateUnitWithUnitIdx(_uData.UnitIdx,TileManager._Instance.GetAIUnitTile(_uData), EnumInfo.TeamType.Opposite);
            return;
        }

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
        UnitData _uD= SearchBestUnit(_gapUnitFeatureInfo[0].FeatureType);
        UnitManager.Instance.CreateUnitWithUnitIdx(_uD.UnitIdx, TileManager._Instance.GetAIUnitTile(_uD), EnumInfo.TeamType.Opposite);


    }
}
