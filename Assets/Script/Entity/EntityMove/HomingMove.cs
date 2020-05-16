using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMove : EntityMove
{
    public override void CallEntity(Unit _tUnit)
    {
        if (_tUnit._tr == null)
            Debug.Log(" if(_tUnit._tr == null)");
        else if (entity.tr == null)
            Debug.Log(" if(_tUnit._tr == null)");

        StartCoroutine(ProcessMove(_tUnit._tr));
    }

    IEnumerator ProcessMove(Transform _tTr)
    {
        while (true)
        {
            yield return null;

            entity.tr.position = Vector2.MoveTowards(entity.tr.position,_tTr.position,Time.deltaTime * entity.entityData.MoveSpeed);
        }
    }
}
