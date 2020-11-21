using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimManager : MonoBehaviour
{
    public Entity entity;

    public Transform tr;
    EntityAnim[] entityAnims;
    EntityAnim curEntityAnim;    
    public void InitEntityAnimMgr(Entity _e)
    {
        entity = _e;
        tr = transform;
        entityAnims = GetComponentsInChildren<EntityAnim>();
        for (int i = 0; i < entityAnims.Length; i++)
            entityAnims[i].InitEntityAnim(_e);

        if (entity.entityData.ReinforceLv != 0)
        {
            curEntityAnim = entityAnims[entity.entityData.ReinforceLv - 1];
        }
        else
        {
            curEntityAnim = entityAnims[0];
        }
        curEntityAnim.gameObject.SetActive(true);
    }

    public void CallEntity(Unit _tUnit)
    {
        curEntityAnim.CallEntity(_tUnit);
    }
    public void CallEntity(Vector2 _v)
    {
        curEntityAnim.CallEntity(_v);
    }
}
