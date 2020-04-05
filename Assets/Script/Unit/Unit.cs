using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;
public class Unit : MonoBehaviour
{
    public string _unitIdx;
    public UnitData _unitData;
    public Transform _tr;

    public TeamType _teamType;
    public Tile _tile;
    public AStarPathTile _aStartTile;

    public StateManager _stateMgr;
    public AnimManager _animMgr;
    public BehaviourManager _behaviourMgr;


    public void InitUnit(TeamType _tType)
    {
        _tr = transform;
        // _unitData = DataManager.Instance.GetUnitDataWithUnitIdx(_unitIdx);
        //_teamType = _tType;
        _tr.position = _tile.transform.position;
        _aStartTile.TakeTile(true);

        _stateMgr = GetComponentInChildren<StateManager>();
        _animMgr = GetComponentInChildren<AnimManager>();
        _behaviourMgr = GetComponentInChildren<BehaviourManager>();
    }


    public void StartBattle()
    {

    }

    public bool CheckAbleToAttack()
    {
        return false;
    }

    public void MoveToTile(Tile _t)
    {
        StartCoroutine(ProcessMoving(_t));
    }

    IEnumerator ProcessMoving(Tile _t)
    {
        while (true)
        {
            _tr.position = Vector2.MoveTowards(_tr.position, _t.transform.position, Time.deltaTime * 60);
            yield return null;
        }
    }
    public void FinishBattle()
    {

    }
   


    
}
