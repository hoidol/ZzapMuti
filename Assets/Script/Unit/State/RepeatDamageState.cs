using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatDamageState : State
{

    public override void InitState(Unit _u)
    {
        base.InitState(_u);
        _state = EnumInfo.State.RepeatDamage;
    }


    public override void StartBattle()
    {
    }

    public override void ChangeState(ChangeState _cS)
    {
        StartCoroutine(ProcessChangeState((ChangeRepeatDamageState)_cS));
    }

    IEnumerator ProcessChangeState(ChangeRepeatDamageState _cS)
    {

        float _timer = _cS._duration;

        Damage _d = new Damage();

        _d.ResourceUnit = _cS._resourceUnit;
        _d.DamagePower = _cS._damage;
        _d.unableToDodge = true;
        
        while (true)
        {
            if (_timer <= 0)
                break;

            _unit._stateMgr.TakeDamage(_d);
            yield return new WaitForSeconds(0.5f);
            _timer -= 0.5f;
        }

    }


}
