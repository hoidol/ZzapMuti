using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
   [HideInInspector] public EnumInfo.State _state;
    public Unit _unit;

    public virtual void InitState(Unit _u)
    {
        _unit = _u;
    }

    public virtual void StartBattle()
    {

    }
    public virtual void ChangeState(ChangeState _cS) {  
    
    }
}
