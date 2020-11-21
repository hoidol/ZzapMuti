
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    [HideInInspector] public Unit _unit;
    [HideInInspector] public bool rootBehaviour;
    [HideInInspector] public EnumInfo.UnitBehaviourType type;
    
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
    public virtual void DoBehaviour(Unit _tUnit=null)
    {

    }




}
