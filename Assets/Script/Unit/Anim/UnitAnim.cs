using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnim : MonoBehaviour
{
    AnimManager animMgr;
    public int reinforceLv;
    Animator anim;

    public void InitUnitAnim(AnimManager _aMgr)
    {
        anim = GetComponent<Animator>();
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
