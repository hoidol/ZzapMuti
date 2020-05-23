using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public string _effectIdx;
   [HideInInspector] public Transform _tr;

    public virtual void InitEffect()
    {
        _tr = transform;
    }

    public virtual void PlayEffect()
    {

    }
}
