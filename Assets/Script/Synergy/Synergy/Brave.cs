using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brave : Synergy
{
    public override void InitSynergy()
    {
        base.InitSynergy();
        _synergyIdx = "Brave";
    }

    public override void ApplySynergy(Unit _u)
    {
        Debug.Log("Brave - ApplySynergy()");
        UnitStatChangeInfo _uStatChangeInfo = new UnitStatChangeInfo();
        _uStatChangeInfo.Arithmetic = EnumInfo.Arithmetic.Add;
        _uStatChangeInfo.UnitStat = EnumInfo.UnitStat.Defence;
        switch (_synergyCount)
        {
            case 2:
                _uStatChangeInfo.Value = 2;
                _u.unitRealData.ApplyUnitStatSynergyChange(_uStatChangeInfo);
                break;
            case 4:
                _uStatChangeInfo.Value = 5;
                _u.unitRealData.ApplyUnitStatSynergyChange(_uStatChangeInfo);

                break;
            case 6:
                _uStatChangeInfo.Value = 9;
                _u.unitRealData.ApplyUnitStatSynergyChange(_uStatChangeInfo);

                break;
        }
    }
}
