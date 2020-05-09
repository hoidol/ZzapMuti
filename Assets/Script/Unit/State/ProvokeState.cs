using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvokeState : State // 도발 상태 
{
   public ProvokeStateInfo _curProvokeStateInfo = new ProvokeStateInfo();
    public override void InitState(Unit _u)
    {
        base.InitState(_u);
        _state = EnumInfo.State.Provoke;
        _curProvokeStateInfo._resourceUnit = null;
    }
    public override void ChangeState(ChangeState _cS)
    {
        _curProvokeStateInfo._resourceUnit = _cS._resourceUnit;
        StopAllCoroutines();
        StartCoroutine( ProcessChangeState((ChangeProvokeState)_cS));
    }

    IEnumerator ProcessChangeState(ChangeProvokeState _cS)
    {
        float _timer = _cS.duration;
        while (_timer >0)
        {
            yield return null;

            if (!_cS._resourceUnit._stateMgr._isLiving)
                break;

            _timer -= Time.deltaTime;
        }
        _curProvokeStateInfo._resourceUnit = null;
    }
}

public class ProvokeStateInfo
{
    public Unit _resourceUnit;
    public float _duration;
}
