using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMove : MonoBehaviour
{
    Entity entity;
    public void InitEntityMove(Entity _e)
    {
        entity = _e;
        _move = GetComponentInChildren<EntityMove>();
    }

    public void CallEntity(Unit _tUnit)
    {

    }

    public void CallEntity(Vector2 _pos)
    {

    }

}
