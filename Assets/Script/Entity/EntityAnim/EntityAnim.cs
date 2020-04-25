using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnim : MonoBehaviour
{
    public Entity entity;
    public virtual void InitEntityAnim(Entity _e)
    {
        entity = _e;
    }

    public virtual void CallEntity(Unit _tUnit)
    {

    }

    public virtual void CallEntity(Vector2 _vec)
    {

    }
}
