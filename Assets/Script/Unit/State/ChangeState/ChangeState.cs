using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    [HideInInspector] public Unit _resourceUnit; // 사용한 유닛
    [HideInInspector] public EnumInfo.State _changeState;

    public virtual void InitChangeState(Unit _u)
    {
        _resourceUnit = _u;
    }
}
