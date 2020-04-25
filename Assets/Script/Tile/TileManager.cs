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
}
