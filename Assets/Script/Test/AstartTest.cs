using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class AstartTest : MonoBehaviour
{

    public Transform _target;


    public float _speed = 10;
    public float _nextWayPointDistance = 0.01f;

    Path _path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;


    Seeker _seeker;

    Transform _tr;
    

    private void Awake()
    {
        _tr = transform;
    }

    private void Start()
    {
        _seeker = GetComponent<Seeker>();
        _seeker.StartPath(_tr.position, _target.position, OnPathComplete);        
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;

            Debug.Log("_path.vectorPath.Count : " + _path.vectorPath.Count);
            for(int i =0;i< _path.vectorPath.Count; i++)
            {
                if (i == _path.vectorPath.Count - 1)
                    break;


                Vector2 _moveVec = new Vector2(_path.vectorPath[i + 1].x - _path.vectorPath[i].x, _path.vectorPath[i + 1].y - _path.vectorPath[i].y);
                //Vector2 _moveVec = new Vector2(_path.vectorPath[i].x - _tr.position.x, _path.vectorPath[i].y - _tr.position.y);
                Debug.Log(_path.vectorPath[i].ToString());
                Debug.Log("_path.vectorPath[i+1] - _path.vectorPath[i] : " + CheckMoveDirection(_moveVec));
            }
            currentWayPoint = 0;
            Debug.Log("OnPathComplete");
            StartCoroutine(ProcessMove(_path.vectorPath[currentWayPoint]));
        }
    }


    IEnumerator ProcessMove(Vector2 _tPos)
    {
        Debug.Log("ProcessMove : " +_tPos);
        while (true)
        {
            _tr.position = Vector2.MoveTowards(_tr.position, _tPos, Time.deltaTime * _speed);
            if(_nextWayPointDistance  > Vector2.SqrMagnitude((Vector2)_tr.position - _tPos))
            {
                break;
            }
            yield return null;
        }
        currentWayPoint++;
        if(currentWayPoint < _path.vectorPath.Count)
            StartCoroutine(ProcessMove(_path.vectorPath[currentWayPoint]));
    }

   
    string CheckMoveDirection(Vector2 _moveVec)
    {
        if (_moveVec.x > 0 && _moveVec.y > 0)
            return "우측 위로";
        else if (_moveVec.x < 0 && _moveVec.y < 0)
            return "좌측 아래로";
        else if (_moveVec.x < 0 && _moveVec.y > 0)
            return "좌측 위로";
        else if (_moveVec.x > 0 && _moveVec.y < 0)
            return "우측 아래로";
        else if (_moveVec.x > 0)
            return "우측";
        else if (_moveVec.x < 0)
            return "좌측";
        else if (_moveVec.y > 0)
            return "위로";
        else if (_moveVec.y < 0)
            return "아래로";
        else
            return "경로없음";
    }

    private void FixedUpdate()
    {
        if (_path == null)
            return;

        if(currentWayPoint >= _path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)_path.vectorPath[currentWayPoint] - (Vector2)_tr.position).normalized;
        
    }
}
