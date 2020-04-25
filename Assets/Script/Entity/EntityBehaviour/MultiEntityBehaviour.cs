using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiEntityBehaviour : EntityBehaviour
{
    public EntityBehaviour[] _nextBehaviours;
    public override void InitEntityBehaviour(Entity _e)
    {
        base.InitEntityBehaviour(_e);
        for (int i = 0; i < _nextBehaviours.Length; i++)
            _nextBehaviours[i].InitEntityBehaviour(_e);
    }
    public override void DoBehaviour(Unit _tUnit)
    {
        for (int i = 0; i < _nextBehaviours.Length; i++)
            _nextBehaviours[i].DoBehaviour(_tUnit);
    }

    public override void DoBehaviour(Vector2 _v)
    {
        for (int i = 0; i < _nextBehaviours.Length; i++)
            _nextBehaviours[i].DoBehaviour(_v);
    }
}
