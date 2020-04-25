using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Minok_Finder : MonoBehaviour
{
    public Unit _unit;
    public Transform _target;
    public Tile _targetTile;

    Path _path;
    Seeker _seeker;

    Transform _tr;

    private Astar_Minok astar_Minok;

    private bool isFinding = false;
    public bool IsFinding
    {
        get { return isFinding; }
    }

    private void Awake()
    {
        _tr = transform;
    }

    private void Start()
    {
        _seeker = GetComponent<Seeker>();

        _unit._tile.gameObject.layer = LayerMask.NameToLayer("TakedTile");
        //Invoke("StartPath",1);
    }

    public float FindTarget()
    {
        Tile[] _allTile=TileManager._Instance.GetAllTile();

        float _minDistance=99;
        int _minTarget=-1;

        for (int i = 0; i < _allTile.Length; i++)
        {
            if (_unit._tile._TilePosIndex == _allTile[i]._TilePosIndex)
                continue;

            if (_allTile[i]._UnitIndex == null)
                continue;

            if (_allTile[i]._UnitIndex._teamType == _unit._teamType)
                continue;

            float _distance = Vector2.Distance(_unit._tile._TilePosIndex, _allTile[i]._TilePosIndex);
 
            if (_distance<_minDistance)
            {
                _minDistance = _distance;
                _minTarget = i;

            }

        }
      
        //_target = _allTile[_minTarget].transform;
        //_target = TileManager._Instance.GetTile((int)_allTile[_minTarget]._TilePosIndex.x - 1, (int)_allTile[_minTarget]._TilePosIndex.y - 1).transform;
        _targetTile = _allTile[_minTarget];

        return _minDistance;
    }

    public void ToPath()
    {
        isFinding = true;
        if (FindTarget() <= 1f)
        {
            isFinding = false;
            return;
        }
        //Star();
         _seeker.StartPath(_tr.position, _targetTile.transform.position, RegistTile);
    }

    public void Star()
    {
        Debug.Log("Start: " + _unit._tile._TilePosIndex);
        Debug.Log("End: " + _targetTile._TilePosIndex);
        astar_Minok = new Astar_Minok(_unit._tile._TilePosIndex, _targetTile._TilePosIndex);
        //astar_Minok.FindPath();

        Debug.Log(astar_Minok.Path.Count);

        Vector2 _to = astar_Minok.Path[0];

        Tile temp = TileManager._Instance.GetTile((int)_to.x, (int)_to.y);

        _unit._tile.SetNothing();
        _unit._tile.gameObject.layer = LayerMask.NameToLayer("Tile");

        temp.SetUnit(_unit);
        temp.gameObject.layer = LayerMask.NameToLayer("TakedTile");

        isFinding = false;
    }

    public void RegistTile(Path p)
    {
        if (!p.error)
        {
            _path = p;

            if (_path.vectorPath.Count < 2)
                return;

            Vector2 _moveVec = new Vector2(_path.vectorPath[1].x - _path.vectorPath[0].x, _path.vectorPath[0 + 1].y - _path.vectorPath[0].y);
            //Vector2 _moveVec = _path.vectorPath[1]-_path.vectorPath[0];

            Vector2 _to = _unit._tile._TilePosIndex + GetMoveDirection(_moveVec);

            Tile temp = TileManager._Instance.GetTile((int)_to.x, (int)_to.y);

            if (temp.gameObject.layer == LayerMask.NameToLayer("TakedTile"))
            {
                _to = _unit._tile._TilePosIndex + GetSecondDirection(_moveVec);
                temp = TileManager._Instance.GetTile((int)_to.x, (int)_to.y);

                if (temp.gameObject.layer == LayerMask.NameToLayer("TakedTile"))
                {
                    isFinding = false;
                    return;
                }
            }
            
            _unit._tile.SetNothing();
            _unit._tile.gameObject.layer = LayerMask.NameToLayer("Tile");

            temp.SetUnit(_unit);
            temp.gameObject.layer= LayerMask.NameToLayer("TakedTile");

            isFinding = false;
        }
    }

    Vector2 GetMoveDirection(Vector2 _moveVec)
    {
        if (_moveVec.x > 0 && _moveVec.y > 0)
        {
            if (_moveVec.x > _moveVec.y)
                return Vector2.right;
            else
                return Vector2.up;
            return new Vector2(1, 1);
        }
        else if (_moveVec.x < 0 && _moveVec.y < 0)
        {
            if (_moveVec.x < _moveVec.y)
                return Vector2.left;
            else
                return Vector2.down;
            return new Vector2(-1, -1);
        }
        else if (_moveVec.x < 0 && _moveVec.y > 0)
        {
            if (Mathf.Abs(_moveVec.x) > Mathf.Abs(_moveVec.y))
                return Vector2.left;
            else
                return Vector2.up;
            return new Vector2(-1, 1);
        }
        else if (_moveVec.x > 0 && _moveVec.y < 0)
        {
            if (Mathf.Abs(_moveVec.x) > Mathf.Abs(_moveVec.y))
                return Vector2.right;
            else
                return Vector2.down;
            return new Vector2(1, -1);
        }
        else if (_moveVec.y > 0)
            return Vector2.up;
        else if (_moveVec.y < 0)
            return Vector2.down;
        else if (_moveVec.x > 0)
            return Vector2.right;
        else if (_moveVec.x < 0)
            return Vector2.left;
        else
            return Vector2.zero;
    }

    public Vector2 GetSecondDirection(Vector2 _moveVec)
    {
        switch( Random.Range(0, 2))
        {
            case 0:
                return Vector2.right;
            case 1:
                return Vector2.left;
        }

        return Vector2.zero;
    }
    
}
