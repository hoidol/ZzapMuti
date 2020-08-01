using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private static TileManager _instance;
    public static TileManager _Instance
    {
        get { return _instance; }
    }

    [SerializeField] private TileGroup _tileGroup;
    public TileGroup _TileGroup
    {
        get { return _tileGroup; }
    }

    public void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        if(_instance==null)
            _instance = this;
    }

    public Tile GetTile(int _xIdx,int _yIdx)
    {
        int _index = _xIdx + _yIdx * _tileGroup._TileWidth;
        return _tileGroup._Tiles[_index];
    }

    public void CreateUnit(string _unitIdx, EnumInfo.TeamType _teamTy)
    {
        _tileGroup.GetCreateAbleTile(_teamTy).SetUnit(_unitIdx, _teamTy);
    }

    public Tile[] GetAllTile()
    {
        return _tileGroup._Tiles;
    }

    public int GetTileIndex(int _xIdx, int _yIdx)
    {
        int _index = _xIdx + _yIdx * _tileGroup._TileWidth;
        return _index;
    }

    public Tile GetTileToMove(Unit _targetUnit,Unit _ownerUnit)
    {
        Vector2 _ownerPos = _ownerUnit._tile._TilePosIndex;
        Vector2 _targerPos = _targetUnit._tile._TilePosIndex;

        float xDistance = _targerPos.x - _ownerPos.x;
        float yDistance = _targerPos.y - _ownerPos.y;

        float xDisAbs = Mathf.Abs(xDistance);
        float yDisAbs = Mathf.Abs(yDistance);
        //y우선인지 x우선인지 탐색
        if (yDisAbs > 1)
        {
        }

        return null;
    }

    public void StartBattle()
    {
        for(int i=0;i< _tileGroup._Tiles.Length;i++)
        {
            _tileGroup._Tiles[i].SetDataBeforeBattle();
        }
    }

    public void EndBattle()
    {
        for (int i = 0; i < _tileGroup._Tiles.Length; i++)
        {
            _tileGroup._Tiles[i].RestoreDataBeforeBattle();
        }
    }

    public Tile GetAssasinMoveTile(EnumInfo.TeamType _searchTeam,bool _startLeft,bool _startBack)
    {
        if(_startLeft&&_startBack)
            return _tileGroup.GetMoveAbleTile_ToRight_ToCenter(_searchTeam);
        else if (!_startLeft && _startBack)
            return _tileGroup.GetMoveAbleTile_ToLeft_ToCenter(_searchTeam);
        else if (!_startLeft && !_startBack)
            return _tileGroup.GetMoveAbleTile_ToLeft_ToBack(_searchTeam);
        else if (_startLeft && !_startBack)
            return _tileGroup.GetMoveAbleTile_ToRight_ToBack(_searchTeam);

        return null;
    }

    public List<Tile> _aiTileList = new List<Tile>();
    public List<Tile> _ableTileList = new List<Tile>();
    public Tile GetAIUnitTile(UnitData _u)
    {        
        if(_aiTileList.Count <= 0)
        {
            for(int i =0;i< _tileGroup._Tiles.Length; i++)
            {
                if (_tileGroup._Tiles[i]._TilePosIndex.y >= 4)
                {
                    Debug.Log("_aiTileList : ");
                    _aiTileList.Add(_tileGroup._Tiles[i]);
                }
            }
        }

        _ableTileList.Clear();
        for (int i=0;i< _aiTileList.Count; i++)
        {
            if (_aiTileList[i]._UnitIndex == null)
                _ableTileList.Add(_aiTileList[i]);
        }

        if (_ableTileList.Count <= 0)
            return null;

        string[] _bestStrs = _u.BestPosition.Split('/');
        string _bestPos = _bestStrs[Random.Range(0, _bestStrs.Length)];
        switch (_bestPos)
        {
            case "TopLeft":
                //왼쪽끝에서 오른쪽 끝으로 체크 자리없으면 아래로(0,7) -> (4,7)
                return GetLeftTile(7);
            case "TopCenter":
                //왼쪽 오른쪽 번갈아가면서 체크 자리 없으면 아래로(2,7) -> (1,7) -> (3,7) ->(0,7) -> (4,7)
                return GetMiddleTile(7);
            case "TopRight":
                //오른쪽 끝에서 왼쪽 끝으로 체크 자리없으면 아래로(4,7) ->(0,7)
                return GetRightTile(7);
            case "MiddleLeft":
                //왼쪽끝에서 오른쪽 끝으로 체크 자리없으면 아래로(0,6) -> (4,6)
                //왼쪽끝에서 오른쪽 끝으로 체크 자리없으면 아래로(0,5) -> (4,5)
                return GetLeftTile(6);
            case "MiddleCenter":
                //왼쪽 오른쪽 번갈아가면서 체크 자리 없으면 아래로(2,6) -> (1,6) -> (3,6) ->(0,6) -> (4,6)
                //왼쪽 오른쪽 번갈아가면서 체크 자리 없으면 아래로(2,5) -> (1,5) -> (3,5) ->(0,5) -> (4,5)
                return GetMiddleTile(6);
            case "MiddleRight":
                //오른쪽 끝에서 왼쪽 끝으로 체크 자리없으면 아래로(4,6) ->(0,6)
                //오른쪽 끝에서 왼쪽 끝으로 체크 자리없으면 아래로(4,5) ->(0,5)
                return GetRightTile(6);
            case "BottomLeft":
                //왼쪽끝에서 오른쪽 끝으로 체크 자리없으면 아래로(0,4) -> (4,4)
                return GetLeftTile(4);
            case "BottomCenter":
                //왼쪽 오른쪽 번갈아가면서 체크 자리 없으면 아래로(2,4) -> (1,4) -> (3,4) ->(0,4) -> (4,4)
                return GetMiddleTile(4);
            case "BottomRight":
                //오른쪽 끝에서 왼쪽 끝으로 체크 자리없으면 아래로(4,4) ->(0,4)
                return GetRightTile(4);
        }
        /*"TopLeft/TopCenter/TopRight
        MiddleLeft / MiddleCenter / MiddleRight
        BottomLeft / BottomCenter / BottomRight"*/
        return null;
    }
    Tile GetAbleTile(int _x, int _y)
    {
        Tile _t = GetTile(_x, _y);
        for (int i =0;i< _ableTileList.Count; i++)
        {
            if (_ableTileList[i]._TilePosIndex.Equals(_t._TilePosIndex))
                return _ableTileList[i];                
        }
        return null;
    }
    Tile GetMiddleTile(int _y) //2
    {
        int _x = 2;
        int _gap = 0;
        while (true)
        {
            Tile _t = GetAbleTile(_x, _y);
            if (_t == null)
            {
                if(_gap >= 2)
                {
                    _y--;
                    if (_y < 4)
                        _y = 7;
                    _gap = 0;
                    continue;
                }

                if(_gap > 0)
                    _gap++;

                _gap = _gap * -1;

                _x = 2;
                _x = _x +_gap;

                continue;
            }
            else
            {
                return _t;
            }
        }
    }

    Tile GetLeftTile(int _y) //2
    {
        int _x = 0;
        int _gap = 0;
        while (true)
        {
            Tile _t = GetAbleTile(_x, _y);
            if (_t == null)
            {
                _gap++;
                if(_x>4)
                {
                    _y--;
                    if (_y < 4)
                        _y = 7;
                    _gap = 0;
                    continue;
                }
                _x = _x + _gap;

                continue;
            }
            else
            {
                return _t;
            }
        }
    }
    Tile GetRightTile(int _y) //2
    {
        int _x = 4;
        int _gap = 0;
        while (true)
        {
            Tile _t = GetAbleTile(_x, _y);
            if (_t == null)
            {
                _gap++;
                if (_x < 0)
                {
                    _y--;
                    if (_y < 4)
                        _y = 7;
                    _gap = 0;
                    continue;
                }
                _x = _x - _gap;
                continue;
            }
            else
            {
                return _t;
            }
        }
    }
}