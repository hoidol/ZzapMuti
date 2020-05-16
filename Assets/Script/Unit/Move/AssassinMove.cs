using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinMove : UnitMove
{
    public override void StartBattle()
    {

    }

    public override void SetPosition()
    {
        if (_unit._teamType.Equals(EnumInfo.TeamType.Red))
        {
            Tile _t = TileManager._Instance.GetAssasinMoveTile(EnumInfo.TeamType.Blue, false, true);
            _unit._tr.position = _t.transform.position;
        }
        else
        {
            _unit._tr.position = TileManager._Instance.GetAssasinMoveTile(EnumInfo.TeamType.Red, false, true).transform.position;
        }
        //

        StartCoroutine(ProcessMove());
    }

    public override void MoveToUnit(Unit _tU)
    {
        _targetUnit = _tU;
    }

    IEnumerator ProcessMove()
    {
        while (true)
        {
            yield return null;

            if (!_unit._needToMove)
                continue;


            _unit._tr.position = Vector2.MoveTowards(_unit._tr.position, _targetUnit._tr.position, Time.deltaTime * 1);
        }
    }


    public virtual void FinishBattle()
    {
        StopAllCoroutines();
    }
}
