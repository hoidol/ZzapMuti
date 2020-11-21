using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimBehaviour : CommonBehaviour
{
    public string _animTrigger;

    public override void DoBehaviour()
    {
        _unit.animMgr.PlayAnim(_animTrigger);
    }

    public override void DoBehaviour(Unit _tUnit)
    {
        _unit.animMgr.PlayAnim(_animTrigger);
    }
}
