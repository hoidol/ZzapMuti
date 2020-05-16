using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForArriveAtUnit : EntityBehaviour
{
    public EntityBehaviour _nextBehaviour;
    public override void InitEntityBehaviour(Entity _e)
    {
        base.InitEntityBehaviour(_e);
        if (_nextBehaviour != null)
            _nextBehaviour.InitEntityBehaviour(_e);
    }
    public override void DoBehaviour(Unit _tUnit)
    {
        StartCoroutine(ProgressArriveAtUnit(_tUnit));
    }
    IEnumerator ProgressArriveAtUnit(Unit _tUnit)
    {
        while (true)
        {
            yield return null;

            if(Vector2.SqrMagnitude(entity.tr.position - _tUnit._tr.position) <= 0.2)
            {
                _nextBehaviour.DoBehaviour(_tUnit);
                break;
            }
        }
    }
    
    public override void DoBehaviour(Vector2 _v)
    {

    }
}
