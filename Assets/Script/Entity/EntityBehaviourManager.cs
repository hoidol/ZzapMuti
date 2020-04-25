using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviourManager : MonoBehaviour
{
    public Entity entity;
    [SerializeField] private EntityBehaviour behaviour;
    public void InitEntityBehaviourMgr(Entity _e)
    {
        entity = _e;
    }

    public void CallEntity(Unit _tUnit)
    {
        behaviour.DoBehaviour(_tUnit);
    }

    public void CallEntity(Vector2 _v)
    {
        behaviour.DoBehaviour(_v);
    }
}
