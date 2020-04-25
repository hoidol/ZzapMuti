using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEntityBehaviour : EntityBehaviour
{
    public override void DoBehaviour(Unit _tUnit)
    {
        entity.DestroyEntity();
    }

    public override void DoBehaviour(Vector2 _v)
    {
        entity.DestroyEntity();
    }

}
