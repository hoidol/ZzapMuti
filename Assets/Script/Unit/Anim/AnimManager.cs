using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    Unit _unit;
    public Transform _tr;

    UnitAnim[] unitAnims;
    UnitAnim curUnitAnim;
    public void InitAnimMgr(Unit _u)
    {
        _tr = transform;
        _unit = _u;
        unitAnims = GetComponentsInChildren<UnitAnim>();

        for (int i = 0; i < unitAnims.Length; i++)
            unitAnims[i].InitUnitAnim(this,i+1);

    }

    public void SetUserData(UnitData _uData, EnumInfo.TeamType _tType)
    {
        curUnitAnim = unitAnims[_uData.ReinforceLv - 1];
        curUnitAnim.SetUserData(_uData, _tType);

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
        curUnitAnim.SetTrigger(_trigger);
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
