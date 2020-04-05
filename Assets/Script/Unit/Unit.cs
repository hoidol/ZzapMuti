using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string _unitIdx;
    public UnitData _unitData;
    public Transform _tr;

    public TeamType _teamType;
    public Tile _tile;

    public StateManager _stateMgr;
    public AnimManager _animMgr;
    public BehaviourManager _behaviourMgr;

    public void InitUnit(TeamType _tType)
    {
        _tr = transform;
        _unitData = DataManager.Instance.GetUnitDataWithUnitIdx(_unitIdx);
        _teamType = _tType;

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

    }

    public void FinishBattle()
    {

    }
   


    
}
