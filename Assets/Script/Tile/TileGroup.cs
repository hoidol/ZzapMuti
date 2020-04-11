using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGroup : MonoBehaviour
{
    [SerializeField] private int _tileWidth;
    public int _TileWidth
    {
        get { return _tileWidth; }
    }

    [SerializeField] private int _tileHeight;
    public int _TileHeight
    {
        get { return _tileHeight; }
    }

    [SerializeField] private Tile[] _tiles;
    public Tile[] _Tiles
    {
        get { return _tiles; }
    }

    [SerializeField] private float _tileDistance;


    public void Awake()
    {
        InitializeTileIndex();
    }

    public void InitializeTileIndex()
    {
        int _height=0;

        _tileHeight = _tiles.Length / _tileWidth;

        for (int i=0;i<_tiles.Length;i++)
        {
            _height = i / _tileWidth;
            _tiles[i].Initialize(new Vector2(i- _height * _tileWidth, _height), _tileDistance);
        }


        //중앙 정렬
        transform.position = new Vector3(-_tileWidth / 2 * _tileDistance, transform.position.y, transform.position.z);
    }

    public Tile GetTile(int _xIdx, int _yIdx)
    {
        int _index = _xIdx + _yIdx * _tileWidth;
        return _tiles[_index];
    }

    public Tile GetCreateAbleTile(TeamType _teamTy)
    {
        Tile tileTemp=null;

        if (_teamTy == TeamType.Red)
        {
            for (int i = 0; i < _tileHeight / 2; i++)
            {
                for (int j = _tileWidth - 1; j >= 0; j--)
                {
                    tileTemp = GetTile(j, i);
                    if (!tileTemp.hasUnit)
                        return tileTemp;
                }
            }
        }
        else if (_teamTy == TeamType.Blue)
        {
            for (int i = _tileHeight-1; i >= _tileHeight / 2; i--)
            {
                for (int j = 0; j < _tileWidth; j++)
                {
                    tileTemp = GetTile(j, i);
                    if (!tileTemp.hasUnit)
                        return tileTemp;
                }
            }
        }

        return null;
    }
}
