using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathTile : MonoBehaviour
{
    public Tile _tile;
    public bool _takedTile;

    public Vector2 _vec2;
    private void Start()
    {
        _tile = GetComponent<Tile>();
        _vec2 = _tile._TilePosIndex;
    } 


    public void TakeTile(bool _b)
    {
        
        _takedTile = _b;
        if (_takedTile)
            gameObject.layer = 8;
        else
            gameObject.layer = 9;
    }
} 