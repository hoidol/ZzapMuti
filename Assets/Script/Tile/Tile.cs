using System.Collections;
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

    private UnitData _unitIndex;
    public UnitData _UnitIndex
    {
        get { return _unitIndex; }
    }
}
