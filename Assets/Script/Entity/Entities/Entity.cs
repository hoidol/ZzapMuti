using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public string Idx;
    public Transform _tr;
    public Unit _ownUnit;
    public EntityAnimManager _animMgr;
    public EntityBehaviourManager _behaviourMgr;
    public EntityMoveManager _moveMgr;
    public void InitEntity()
    {
        _tr = transform;

        _animMgr = GetComponentInChildren<EntityAnimManager>();
        _behaviourMgr = GetComponentInChildren<EntityBehaviourManager>();
        _moveMgr = GetComponentInChildren<EntityMoveManager>();

        _animMgr.InitEntityAnimMgr(this);
        _behaviourMgr.InitEntityBehaviourMgr(this);
        _moveMgr.InitEntityMoveMgr(this);
    }


    public void CallEntity(Unit _u, Unit _tUnit)
    {
        _ownUnit = _u;

        _animMgr.CallEntity(_tUnit);
        _behaviourMgr.CallEntity(_tUnit);
        _moveMgr.CallEntity(_tUnit);

    }

    public void CallEntity(Unit _u, Vector2 _v)
    {
        _ownUnit = _u;

        _animMgr.CallEntity(_v);
        _behaviourMgr.CallEntity(_v);
        _moveMgr.CallEntity(_v);
    }
}
