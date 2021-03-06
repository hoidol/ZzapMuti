﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEntityBehaviour : EntityBehaviour
{
    public EntityBehaviour _nextBehaviour;
    public EnumInfo.TargetTeam TargetTeam;

    public override void InitEntityBehaviour(Entity _e)
    {
        base.InitEntityBehaviour(_e);
        if (_nextBehaviour != null)
            _nextBehaviour.InitEntityBehaviour(_e);
    }
    public override void DoBehaviour(Unit _tUnit)
    {

    }


    public override void DoBehaviour(Vector2 _v)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Unit"))
        {

            Unit _targetUnit = collision.GetComponent<Unit>();
            switch (TargetTeam)
            {
                case EnumInfo.TargetTeam.SameTeam:
                    if(entity.ownUnit._teamType == _targetUnit._teamType)
                        _nextBehaviour.DoBehaviour(_targetUnit);
                    
                    break;
                case EnumInfo.TargetTeam.OppositeTeam:
                    if (entity.ownUnit._teamType != _targetUnit._teamType)
                        _nextBehaviour.DoBehaviour(_targetUnit);
                    break;
                case EnumInfo.TargetTeam.Both:
                    _nextBehaviour.DoBehaviour(_targetUnit);
                    break;
            }
        }
    }

}
