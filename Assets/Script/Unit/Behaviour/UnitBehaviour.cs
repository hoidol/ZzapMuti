
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    public bool _nonTarget;

    [HideInInspector] public Unit _unit;
    
    public virtual void InitUnitBehaviour(Unit _u)
    {
        _unit = _u;
    }
    
    public virtual void SetUserData(UnitData _uData, EnumInfo.TeamType _tType)
    {

    }
    public virtual void StartBattle()
    {

    }

   public virtual void StartBehaviour()
    {

    }
    public virtual void DoBehaviour()
    {

    }
    public virtual void DoBehaviour(Unit _tUnit)
    {

    }




}
