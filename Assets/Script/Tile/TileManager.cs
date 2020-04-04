﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private static TileManager _instance;
    public static TileManager _Instance;

    [SerializeField] private TileGroup _tileGroup;

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

}