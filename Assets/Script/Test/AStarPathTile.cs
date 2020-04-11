using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathTile : MonoBehaviour
{
    public Tile _tile;
    public bool _takedTile;

    public Vector2 _vec2;
    public Unit _ownUnit;
    private void Start()
    {
        _tile = GetComponent<Tile>();
        _vec2 = _tile._TilePosIndex;
    } 


    public void TakeTile(Unit _u, bool _b)
    {
        _ownUnit = _u;
        _takedTile = _b;
        if (_takedTile)
            gameObject.layer = 8;
        else
            gameObject.layer = 9;
    }
} 