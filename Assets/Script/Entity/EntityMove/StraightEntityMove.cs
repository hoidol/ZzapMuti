﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightEntityMove : EntityMove
{

    public override void CallEntity(Unit _tUnit)
    {
        if (_tUnit._tr == null)
            Debug.Log(" if(_tUnit._tr == null)");
        else if (entity.tr == null)
            Debug.Log(" if(_tUnit._tr == null)");

        StartCoroutine(ProcessMove((_tUnit._tr.position - entity.tr.position).normalized));
    }

    public override void CallEntity(Vector2 _pos)
    {
        StartCoroutine(ProcessMove((_pos - (Vector2)entity.tr.position).normalized));
    }

    IEnumerator ProcessMove(Vector3 _dir)
    {
        while (true)
        {
            yield return null;
            entity.tr.position += _dir * Time.deltaTime * entity.entityData.MoveSpeed;
        }
    }
}
