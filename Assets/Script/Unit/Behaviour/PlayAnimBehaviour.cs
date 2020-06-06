using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimBehaviour : UnitBehaviour
{
    public string _animTrigger;

    public override void DoBehaviour()
    {
        _unit._animMgr.PlayAnim(_animTrigger);
    }

    public override void DoBehaviour(Unit _tUnit)
    {
        _unit._animMgr.PlayAnim(_animTrigger);
    }
}
