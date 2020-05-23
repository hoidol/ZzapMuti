using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBehaviour : UnitBehaviour
{
    public UnitBehaviour[] _nextBehaviours;

    public override void InitUnitBehaviour(Unit _u)
    {
        base.InitUnitBehaviour(_u);

        for (int i = 0; i < _nextBehaviours.Length; i++)
            _nextBehaviours[i].InitUnitBehaviour(_u);
    }
    public override void StartBattle()
    {
        for (int i = 0; i < _nextBehaviours.Length; i++)
            _nextBehaviours[i].StartBattle();
    }
    public override void DoBehaviour()
    {

        for (int i = 0; i < _nextBehaviours.Length; i++)
            _nextBehaviours[i].DoBehaviour();
    }

    public override void DoBehaviour(Unit _tUnit)
    {
        for (int i = 0; i < _nextBehaviours.Length; i++)
            _nextBehaviours[i].DoBehaviour(_tUnit);
    }
}
