using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEntityAnim : EntityAnim
{
    public int reinforceLv;
    public override void InitEntityAnim(Entity _e)
    {
        base.InitEntityAnim(_e);
    }

    public override void CallEntity(Unit _tUnit)
    {
        switch (type)
        {
            case EnumInfo.EntityAnimType.NONE:
                StartCoroutine(ProcessLookAt(_tUnit));
                break;
        }
    }

    public override void CallEntity(Vector2 _vec)
    {
        switch (type)
        {
            case EnumInfo.EntityAnimType.NONE:
                StartCoroutine(ProcessLookAt(_vec));
                break;
        }
    }


    IEnumerator ProcessLookAt(Unit _tUnit)
    {
        while (true)
        {
         
            Vector2 dir = _tUnit._tr.position - entity.tr.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            entity.animMgr.tr.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator ProcessLookAt(Vector2 _v)
    {
        while (true)
        {
            Vector2 dir = _v - (Vector2)entity.tr.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            entity.animMgr.tr.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            yield return new WaitForSeconds(0.1f);
        }

    }
}
