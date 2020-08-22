using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayTypeCC : AIPlayType
{
    public override void InitAIPlayType()
    {
        _aiPlayType = EnumInfo.AIPlayType.CC;
    }
    public override void StartTurn(int _r)
    {
        base.StartTurn(_r);

        if (_selectedUnitData.Count <= 0)
            return;
        if (_r <= 1)
        {
            UnitData _uData = _selectedUnitData[Random.Range(0, _selectedUnitData.Count)];
            UnitManager.Instance.CreateUnitWithUnitIdx(_uData.UnitIdx, TileManager._Instance.GetAIUnitTile(_uData), EnumInfo.TeamType.Opposite);
            return;
        }

        AnalyzeSituation();


        // 상황 분석
        _gapUnitFeatureInfo.Sort(delegate (UnitFeatureInfo a, UnitFeatureInfo b) //가장 부족한 부분 찾는거잖아
        {
            if (a.Value > b.Value)
                return -1;
            if (a.Value < b.Value)
                return 1;
            return 0;
        });

        UnitData _uD = null;
        for (int i = 0; i < _gapUnitFeatureInfo.Count; i++)
        {
            if (_gapUnitFeatureInfo[i].Value >= 10) // 능력 차이가 많이 나면 성향 보다는 상황에 맞게 선택해!
            {
                _uD = SearchBestUnit(_gapUnitFeatureInfo[0].FeatureType);
                UnitManager.Instance.CreateUnitWithUnitIdx(_uD.UnitIdx, TileManager._Instance.GetAIUnitTile(_uD), EnumInfo.TeamType.Opposite);
                return;
            }
        }

        _uD = SearchBestUnit(EnumInfo.UnitFeatureType.Feature_CC);
        UnitManager.Instance.CreateUnitWithUnitIdx(_uD.UnitIdx, TileManager._Instance.GetAIUnitTile(_uD), EnumInfo.TeamType.Opposite);

    }
}
