using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShieldBehaviour : UnitBehaviour
{
    public float _shieldRateWithHp;


    public override void DoBehaviour()
    {
        _unit._stateMgr.ChargeShield(_unit._unitData.Hp * _shieldRateWithHp);
    }

    public override void DoBehaviour(Unit _tUnit)
    {
        _unit._stateMgr.ChargeShield(_unit._unitData.Hp * _shieldRateWithHp);
    }
}
