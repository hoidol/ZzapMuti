using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnim : MonoBehaviour
{
    AnimManager animMgr;
    int reinforceLv;
    Animator anim;

    public void InitUnitAnim(AnimManager _aMgr, int _rLv)
    {

           anim = GetComponent<Animator>();
        reinforceLv = _rLv;
        animMgr = _aMgr;
    }

    public void SetUserData(UnitData _uData,EnumInfo.TeamType _tType)
    {

    }

    public void SetTrigger(string _t)
    {
        anim.SetTrigger(_t);
    }


}
