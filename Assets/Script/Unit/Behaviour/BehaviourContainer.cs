using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourContainer : MonoBehaviour
{
    Unit unit;
   [HideInInspector] public int reinforceLv;

    [HideInInspector] public UnitBehaviour normal;
    [HideInInspector] public UnitBehaviour skill;
    [HideInInspector] public UnitBehaviour passive;

    public virtual void InitBehaviourContainer(Unit _u,int _rLv)
    {
        unit = _u;
        reinforceLv = _rLv;
        UnitBehaviour[] _uBehaviours = GetComponentsInChildren<UnitBehaviour>();
        for (int i = 0; i < _uBehaviours.Length; i++)
        {
            if (_uBehaviours[i].rootBehaviour)
            {
                if (_uBehaviours[i].type == EnumInfo.UnitBehaviourType.NORMAL)
                    normal = _uBehaviours[i];
                else if (_uBehaviours[i].type == EnumInfo.UnitBehaviourType.SKILL)
                    skill = _uBehaviours[i];
                else if (_uBehaviours[i].type == EnumInfo.UnitBehaviourType.PASSIVE)
                    passive = _uBehaviours[i];

            }
        }
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
    public virtual void NormalBehaviour(Unit _targetUnit = null)
    {
        normal.DoBehaviour(_targetUnit);
    }

    public virtual void SkillBehaviour(Unit _targetUnit = null)
    {
        skill.DoBehaviour(_targetUnit);
    }

    public virtual void PassiveBehaviour(Unit _targetUnit = null)
    {
        passive.DoBehaviour(_targetUnit);
    }

}
