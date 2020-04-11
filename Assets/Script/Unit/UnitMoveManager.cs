using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;
using System;

public class UnitMoveManager : MonoBehaviour
{
    private static UnitMoveManager _instance;

    public static UnitMoveManager Instance
    {
        get
        {
            return _instance;
        }
    }
    public Seeker _seeker;
    public AStarPathTile[] _aStarPathTiles;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        _seeker = GetComponent<Seeker>();
    }
    public AStarPathTile GetTile(int _xIdx, int _yIdx)
    {
        int _index = _xIdx + _yIdx * 5;
        return _aStarPathTiles[_index];
    }

    public void GetMoveUnitNextTile(Unit _unit,Action _finishCallBack)
    {
       // _unit._aStartTile.TakeTile(false);
        AstarPath.active.Scan();
        _curUnit = _unit;

       NextMoveDirection(_unit);       
    }

    List<TargetUnitDistance> _tUnitDis = new List<TargetUnitDistance>();
    Unit _curUnit;
    Unit _targetUnit;
    void NextMoveDirection(Unit _unit)
    {
        _tUnitDis.Clear();

        for (int i = 0; i < UnitManager.Instance._curUnitsOnTile.Count; i++)
        {
            if (_unit._teamType == UnitManager.Instance._curUnitsOnTile[i]._teamType)
                continue;
            _seeker.StartPath(_unit._tr.position, UnitManager.Instance._curUnitsOnTile[i]._tr.position, OnPathComplete);
        }       
    }


    void OnPathComplete(Path _path)
    {
        Debug.Log("GetNextMoveDirection Checking Path : " + _path.vectorPath.Count);
        if (_path.vectorPath.Count > 0)
        {
            TargetUnitDistance _tUDis = new TargetUnitDistance();
            //_tUDis.TargetUnit = UnitManager.Instance._curUnitsOnTile[i];
            _tUDis.DistanceCount = _path.vectorPath.Count;
            _tUDis.Path = _path;
            _tUnitDis.Add(_tUDis);
        }
        if (_tUnitDis.Count > 0)
        {
            TargetUnitDistance _shortestDis = _tUnitDis[0];
            for (int i = 0; i < _tUnitDis.Count; i++)
            {
                if (_shortestDis.DistanceCount > _tUnitDis[i].DistanceCount)
                {
                    _shortestDis = _tUnitDis[i];
                }
            }
            AStarPathTile _nextAStarTile = null;

            switch (GetMoveDirection(_shortestDis.Path.vectorPath[0]))
            {
                case MoveDirection.Up:
                    _nextAStarTile = GetTile((int)_curUnit._aStartTile._vec2.x, (int)_curUnit._aStartTile._vec2.y + 1);
                    break;
                case MoveDirection.Down:
                    _nextAStarTile = GetTile((int)_curUnit._aStartTile._vec2.x, (int)_curUnit._aStartTile._vec2.y - 1);
                    break;
                case MoveDirection.Left:
                    _nextAStarTile = GetTile((int)_curUnit._aStartTile._vec2.x + 1, (int)_curUnit._aStartTile._vec2.y);
                    break;
                case MoveDirection.Right:
                    _nextAStarTile = GetTile((int)_curUnit._aStartTile._vec2.x - 1, (int)_curUnit._aStartTile._vec2.y);
                    break;
                case MoveDirection.UpLeft:
                    _nextAStarTile = GetTile((int)_curUnit._aStartTile._vec2.x + 1, (int)_curUnit._aStartTile._vec2.y + 1);
                    break;
                case MoveDirection.UpRight:
                    _nextAStarTile = GetTile((int)_curUnit._aStartTile._vec2.x - 1, (int)_curUnit._aStartTile._vec2.y + 1);
                    break;
                case MoveDirection.DownLeft:
                    _nextAStarTile = GetTile((int)_curUnit._aStartTile._vec2.x + 1, (int)_curUnit._aStartTile._vec2.y - 1);
                    break;
                case MoveDirection.DownRight:
                    _nextAStarTile = GetTile((int)_curUnit._aStartTile._vec2.x - 1, (int)_curUnit._aStartTile._vec2.y - 1);
                    break;
                case MoveDirection.Stop:
                    _nextAStarTile = GetTile((int)_curUnit._aStartTile._vec2.x, (int)_curUnit._aStartTile._vec2.y);
                    break;
            }
            //_curUnit.MoveToTile(_nextAStarTile._tile);
        }
    }


    MoveDirection GetMoveDirection(Vector2 _moveVec)
    {
        if (_moveVec.x > 0 && _moveVec.y > 0)
            return MoveDirection.DownRight;
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
}


//class TargetUnitDistance
//{
//    public int DistanceCount;
//    public Path Path;
//}


public class PathUnit
{
    public Path Path;
    public Unit Unit;
}