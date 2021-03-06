﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimManager : MonoBehaviour
{
    public Entity entity;

    public Transform tr;
    EntityAnim entityAnim;
    
    public void InitEntityAnimMgr(Entity _e)
    {
        entity = _e;
        tr = transform;
        entityAnim = GetComponentInChildren<EntityAnim>();
        entityAnim.InitEntityAnim(_e);
    }

    public void CallEntity(Unit _tUnit)
    {
        entityAnim.CallEntity(_tUnit);
    }
    public void CallEntity(Vector2 _v)
    {
        entityAnim.CallEntity(_v);
    }
}
