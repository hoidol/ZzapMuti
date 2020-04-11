using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    Unit _unit;
    public void InitMoveMgr(Unit _u)
    {
        _unit = _u; 
    }

    Action<AStarPathTile> _moveCallback;
    public void GetNextTile(Action<AStarPathTile> _a)
    {
        _moveCallback = _a;

        Debug.Log("Unit - MoveManager - GetNextTile()");
        Unit _shortestUnit =  null;
        float _shortestDis = float.MaxValue;
        for (int i = 0; i < UnitManager.Instance._curUnitsOnTile.Count; i++)
        {
            if (_unit._teamType == UnitManager.Instance._curUnitsOnTile[i]._teamType)
                continue;

            float _dis = Vector2.SqrMagnitude(_unit._tr.position - UnitManager.Instance._curUnitsOnTile[i]._tr.position);
            if (_dis < _shortestDis)
            {
                _shortestUnit = UnitManager.Instance._curUnitsOnTile[i];
                _shortestDis = _dis;
            }
        }

        if(_shortestUnit != null)
            _unit._seeker.StartPath(_unit._aStartTile.transform.position, _shortestUnit._aStartTile.transform.position, OnPathComplete);
    }


    void OnPathComplete(Path _path)
    {
        AStarPathTile _nextAStarTile = null;
        int _nextX = 0;
        int _nextY = 0;
        Debug.Log("Unit Name : " + _unit.name);
        for (int i = 0; i < _path.vectorPath.Count; i++)
            Debug.Log("_path.vectorPath[" + i + "] : " + (_path.vectorPath[i]-_unit._tr.position));

        if (_path.vectorPath.Count > 0)
         {
               Debug.Log("_path.vectorPath[0] - _unit._tr.position : " + (_path.vectorPath[1] - _unit._tr.position) + "Unit Name : " + _unit.name);               
                switch (GetMoveDirection(_path.vectorPath[1] - _path.vectorPath[0]))
                {
                    case MoveDirection.Up:
                    _nextX = (int)_unit._aStartTile._vec2.x;
                    _nextY = (int)_unit._aStartTile._vec2.y + 1;
                        break;
                    case MoveDirection.Down:
                    _nextX = (int)_unit._aStartTile._vec2.x;
                    _nextY = (int)_unit._aStartTile._vec2.y - 1;
                        break;
                    case MoveDirection.Left:
                    _nextX = (int)_unit._aStartTile._vec2.x-1;
                    _nextY = (int)_unit._aStartTile._vec2.y;
                        break;
                    case MoveDirection.Right:
                    _nextX = (int)_unit._aStartTile._vec2.x+1;
                    _nextY = (int)_unit._aStartTile._vec2.y ;
                        break;
                    case MoveDirection.UpLeft:
                    _nextX = (int)_unit._aStartTile._vec2.x-1;
                    _nextY = (int)_unit._aStartTile._vec2.y + 1;
                        break;
                    case MoveDirection.UpRight:
                    _nextX = (int)_unit._aStartTile._vec2.x+1;
                    _nextY = (int)_unit._aStartTile._vec2.y  +1;
                        break;
                    case MoveDirection.DownLeft:
                    _nextX = (int)_unit._aStartTile._vec2.x -1;
                    _nextY = (int)_unit._aStartTile._vec2.y - 1;
                        break;
                    case MoveDirection.DownRight:
                    _nextX = (int)_unit._aStartTile._vec2.x+1;
                    _nextY = (int)_unit._aStartTile._vec2.y - 1;
                        break;
                    case MoveDirection.Stop:
                    _nextX = (int)_unit._aStartTile._vec2.x;
                    _nextY = (int)_unit._aStartTile._vec2.y;
                        break;
                }
          }
        else
        {
            _nextX = (int)_unit._aStartTile._vec2.x;
            _nextY = (int)_unit._aStartTile._vec2.y;
        }

        _nextAStarTile = GetTile(_nextX, _nextY);
        if(_unit._aStartTile != _nextAStarTile)
        {
            if (_nextAStarTile._takedTile)
                _nextAStarTile = _unit._aStartTile;
        }
        _moveCallback.Invoke(_nextAStarTile);
        
    }
    MoveDirection GetMoveDirection(Vector2 _moveVec)
    {
        if (_moveVec.x > 0f && _moveVec.y > 0)
            return MoveDirection.UpRight;
        else if (_moveVec.x < 0 && _moveVec.y < 0)
            return MoveDirection.DownLeft;
        else if (_moveVec.x < 0 && _moveVec.y > 0)
            return MoveDirection.UpLeft;
        else if (_moveVec.x > 0 && _moveVec.y < 0)
            return MoveDirection.DownRight;
        else if (_moveVec.x > 0)
            return MoveDirection.Right;
        else if (_moveVec.x < 0)
            return MoveDirection.Left;
        else if (_moveVec.y > 0)
            return MoveDirection.Up;
        else if (_moveVec.y < 0)
            return MoveDirection.Down;
        else
            return MoveDirection.Stop;
    }
    public AStarPathTile GetTile(int _xIdx, int _yIdx)
    {
        int _index = _xIdx + _yIdx * 5;
        return UnitMoveManager.Instance._aStarPathTiles[_index];
    }



}

public class TargetUnitDistance
{
    public int DistanceCount;
    public Path Path;
}


public enum MoveDirection
{
    Up,
    Down,
    Left,
    Right,
    UpLeft,
    UpRight,
    DownLeft,
    DownRight,
    Stop
}