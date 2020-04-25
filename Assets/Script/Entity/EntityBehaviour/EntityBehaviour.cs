using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    public Entity entity;
    public virtual void InitEntityBehaviour(Entity _e)
    {
        entity = _e;
    }
    public virtual void DoBehaviour(Unit _tUnit)
    {

    }


    public virtual void DoBehaviour(Vector2 _v)
    {

    }
}
