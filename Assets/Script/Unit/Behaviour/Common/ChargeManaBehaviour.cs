using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeManaBehaviour : CommonBehaviour
{

    public override void DoBehaviour()
    {
        _unit.stateMgr.ChargeMana();
    }
    public override void DoBehaviour(Unit _tUnit)
    {

        _tUnit.stateMgr.ChargeMana();
    }
}
