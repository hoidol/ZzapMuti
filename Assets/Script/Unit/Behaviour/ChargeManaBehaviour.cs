using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeManaBehaviour : UnitBehaviour
{

    public override void DoBehaviour()
    {
        _unit._stateMgr.ChargeMana();
    }
    public override void DoBehaviour(Unit _tUnit)
    {
        Debug.Log("때려서 마나 충전하기");
        _tUnit._stateMgr.ChargeMana();
    }
}
