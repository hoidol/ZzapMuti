using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMoveManager : MonoBehaviour
{
    Entity entity;
    [SerializeField] EntityMove move;
    
    public void InitEntityMoveMgr(Entity _e)
    {
        entity = _e;
        move = GetComponentInChildren<EntityMove>();
    }

    public void CallEntity(Unit _tUnit)
    {
        move.CallEntity(_tUnit);
    }

    public void CallEntity(Vector2 _v)
    {
        move.CallEntity(_v);
    }

}
