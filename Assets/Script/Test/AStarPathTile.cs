using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathTile : MonoBehaviour
{
    public Tile _tile;
    public bool _takedTile;
    private void Awake()
    {
        _tile = GetComponent<Tile>();
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