using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public string Idx;
    public EntityData entityData;
    public Transform tr;
    public Unit ownUnit;
    public EntityAnimManager animMgr;
    public EntityBehaviourManager behaviourMgr;
    public EntityMoveManager moveMgr;
    public void InitEntity()
    {
        tr = transform;
        
        entityData = DataManager.Instance.GetEntityDataWithIdx(Idx);
        animMgr = GetComponentInChildren<EntityAnimManager>();
        behaviourMgr = GetComponentInChildren<EntityBehaviourManager>();
        moveMgr = GetComponentInChildren<EntityMoveManager>();

        animMgr.InitEntityAnimMgr(this);
        behaviourMgr.InitEntityBehaviourMgr(this);
        moveMgr.InitEntityMoveMgr(this);
    }


    public void CallEntity(Unit _u, Unit _tUnit)
    {
        ownUnit = _u;

        animMgr.CallEntity(_tUnit);
        behaviourMgr.CallEntity(_tUnit);
        moveMgr.CallEntity(_tUnit);

        StartCoroutine(ProcessDuration());
    }

    public void CallEntity(Unit _u, Vector2 _v)
    {
        ownUnit = _u;

        animMgr.CallEntity(_v);
        behaviourMgr.CallEntity(_v);
        moveMgr.CallEntity(_v);
        StartCoroutine(ProcessDuration()); 
    }

    IEnumerator ProcessDuration()
    {
        yield return new WaitForSeconds(entityData.Duration);
        DestroyEntity();
    }

    public void DestroyEntity()
    {
        gameObject.SetActive(false);
    }
}
