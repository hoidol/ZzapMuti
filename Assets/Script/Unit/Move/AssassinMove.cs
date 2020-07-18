using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinMove : UnitMove
{
    Tile _reservedTile;
    public override void StartBattle()
    {

    }

    public override void SetPosition()
    {
        if (_unit._teamType.Equals(EnumInfo.TeamType.Player))
        {
            _reservedTile = TileManager._Instance.GetAssasinMoveTile(EnumInfo.TeamType.Opposite, false, true);
        }
        else
        {
            _reservedTile = TileManager._Instance.GetAssasinMoveTile(EnumInfo.TeamType.Player, false, true);
        }

        if (_reservedTile)
        {
            _reservedTile._ReservationUnit = _unit;
            _unit._tr.position = _reservedTile.transform.position;
        }

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


            _unit._tr.position = Vector2.MoveTowards(_unit._tr.position, _targetUnit._tr.position, Time.deltaTime * _moveSpeedState.GetMoveSpeed());
        }
    }

    public override void RestorePosition()
    {
        if (_reservedTile)
        {
            _reservedTile._ReservationUnit = null;;
        }
    }

    public override void FinishBattle()
    {
        StopAllCoroutines();
    }
}
