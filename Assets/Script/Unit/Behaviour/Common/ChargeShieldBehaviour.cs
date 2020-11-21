using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShieldBehaviour : CommonBehaviour
{
    public float _shieldRateWithHp;


    public override void DoBehaviour()
    {
        _unit.stateMgr.ChargeShield(_unit.unitData.Hp * _shieldRateWithHp);
    }

    public override void DoBehaviour(Unit _tUnit)
    {
        _unit.stateMgr.ChargeShield(_unit.unitData.Hp * _shieldRateWithHp);
    }
}
