using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtEntityAnim : EntityAnim
{

    public override void CallEntity(Unit _tUnit)
    {
        StartCoroutine(ProcessLookAt(_tUnit));
    }

    public override void CallEntity(Vector2 _vec)
    {
        StartCoroutine(ProcessLookAt(_vec));
    }

    IEnumerator ProcessLookAt(Unit _tUnit)
    {
        while (true)
        {
            yield return null;
            Vector2 dir = _tUnit._tr.position - entity.tr.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            entity.animMgr.tr.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    IEnumerator ProcessLookAt(Vector2 _v)
    {
        while (true)
        {
            yield return null;
            Vector2 dir = _v - (Vector2)entity.tr.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            entity.animMgr.tr.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }

}
