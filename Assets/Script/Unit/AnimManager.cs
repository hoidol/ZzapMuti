using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    Unit _unit;
    public Transform _tr;
    public Animator _anim;
    public void InitAnimMgr(Unit _u)
    {
        _tr = transform;
        _unit = _u;
        _anim = GetComponentInChildren<Animator>();

        if (_unit._teamType.Equals(EnumInfo.TeamType.Player))
            UpdateDirection(Vector2.up);
        else
            UpdateDirection(-Vector2.up);
    }

    public void StartBattle()
    {

    }

    public void UpdateDirection(Vector2 _v)
    {
        if (_v != Vector2.zero)
        {
            float angle = Mathf.Atan2(_v.y, _v.x) * Mathf.Rad2Deg;
            _tr.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void PlayAnim(string _trigger)
    {
        _anim.SetTrigger(_trigger);
    }
    public void FinishBattle()
    {
        if (_unit._teamType.Equals(EnumInfo.TeamType.Player))
            UpdateDirection(Vector2.up);
        
        else
            UpdateDirection(-Vector2.up);
    }


    public void Die()
    {
        StopAllCoroutines();
    }
}
