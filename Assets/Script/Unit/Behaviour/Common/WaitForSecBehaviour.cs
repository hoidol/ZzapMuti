using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForSecBehaviour : CommonBehaviour
{
    public float _waitForSec;
    public UnitBehaviour _nextBehaviour;
    public override void InitUnitBehaviour(Unit _u)
    {
        base.InitUnitBehaviour(_u);

        _nextBehaviour.InitUnitBehaviour(_u);
    }
    public override void StartBattle()
    {
        if (_nextBehaviour)
            _nextBehaviour.StartBattle();
    }
    public override void DoBehaviour()
    {
        StartCoroutine(ProcessWaitForSec());
    }

    public override void DoBehaviour(Unit _tUnit)
    {
        StartCoroutine(ProcessWaitForSec(_tUnit));
    }

    IEnumerator ProcessWaitForSec()
    {
        yield return new WaitForSeconds(_waitForSec);
        _nextBehaviour.DoBehaviour();

    }
    IEnumerator ProcessWaitForSec(Unit _tUnit)
    {
        yield return new WaitForSeconds(_waitForSec);
        _nextBehaviour.DoBehaviour(_tUnit);

    }
}
