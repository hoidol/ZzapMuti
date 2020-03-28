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
}
