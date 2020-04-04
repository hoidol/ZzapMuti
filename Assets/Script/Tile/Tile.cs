﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _tileSpriteRenderer;
    public SpriteRenderer _TileSpriteRenderer
    {
        get { return _tileSpriteRenderer; }
    }

    [SerializeField] private Vector2 _tilePosIndex=new Vector2();
    public Vector2 _TilePosIndex
    {
        get { return _tilePosIndex; }
    }

    public void Initialize(Vector2 _tilePosIdx, float _distance)
    {
        _tilePosIndex = _tilePosIdx;

        transform.localPosition = _tilePosIdx * _distance;
    }

    private Unit _unitIndex;
    public Unit _UnitIndex
    {
        get { return _unitIndex; }
    }

    public bool hasUnit = false;

    public void SetUnit(Unit _unit)
    {
        _unitIndex = _unit;

        if (_unit == null)
            _tileSpriteRenderer.color = Color.white;
        else
            _tileSpriteRenderer.color = Color.black;
    }
    public void SetUnit(string _unitIdx)
    {
        UnitData _uniData= DataManager.Instance.GetUnitDataWithUnitIdx(_unitIdx);

        hasUnit = true;
        //_unitIndex = UnitManager.Instance.CreateUnitWithUnitIdx(_unitIdx,TeamType.Rad);

        _tileSpriteRenderer.color = Color.black;
    }
}
