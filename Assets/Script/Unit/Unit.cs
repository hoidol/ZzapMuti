using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;
using System;

public class Unit : MonoBehaviour
{
    public string _unitIdx;
    public UnitData _unitData;
    public Transform _tr;
    public EnumInfo.TeamType _teamType;
    public Tile _tile;

    [HideInInspector] public StateManager _stateMgr;
    [HideInInspector] public AnimManager _animMgr;
    [HideInInspector] public BehaviourManager _behaviourMgr;
    [HideInInspector] public MoveManager _moveMgr;

    public Seeker _seeker;

    public Damage _normalDamage;
    public Damage _skillDamage;

    public List< CharacterInfoData> _characterInfoDataList= new List<CharacterInfoData>();
    public void InitUnit(EnumInfo.TeamType _tType)
    {
        _tr = transform;
        _unitData = DataManager.Instance.GetUnitDataWithUnitIdx(_unitIdx);
        _teamType = _tType;

        _stateMgr = GetComponentInChildren<StateManager>();
        _animMgr = GetComponentInChildren<AnimManager>();
        _behaviourMgr = GetComponentInChildren<BehaviourManager>();
        _moveMgr = GetComponentInChildren<MoveManager>();
        _seeker = GetComponentInChildren<Seeker>();

        _stateMgr.InitStateMgr(this);
        _animMgr.InitAnimMgr(this);
        _behaviourMgr.InitBehaviourMgr(this);
        _moveMgr.InitMoveMgr(this);


        if (_unitData.DamageType.Equals("Physic"))
            _normalDamage.Type = EnumInfo.DamageType.Physic;
        else
            _normalDamage.Type = EnumInfo.DamageType.Magic;
        _normalDamage.DamagePower = _unitData.Damage;
        _normalDamage.ResourceUnit = this;

        if (_unitData.SkillDamageType.Equals("Physic"))
            _skillDamage.Type = EnumInfo.DamageType.Physic;
        else
            _skillDamage.Type = EnumInfo.DamageType.Magic;

        _skillDamage.DamagePower = _unitData.SkillDamage;
        _skillDamage.ResourceUnit = this;

        if (!_unitData.Character.Equals("0"))
        {
            string[] _chars = _unitData.Character.Split('/');
            for (int i = 0; i < _chars.Length; i++)
            {
                _characterInfoDataList.Add(DataManager.Instance.GetCharacterInfoDataWithCharacter(_chars[i]));
            }
        }
       
    }

    public void SetTile(Tile _t)
    {
        _tile = _t;
        _tr.position = _tile.transform.position;
    }

    public void StartBattle()
    {
        _targetUnit = null;
        _stateMgr.StartBattle();
        _animMgr.StartBattle();
        _behaviourMgr.StartBattle();
        _moveMgr.StartBattle();
        _tr.position = _tile.transform.position;
    }

    public void SetPosition()
    {
        _moveMgr.SetPosition();
    }

    public bool _ableToAttack;
    public bool _needToMove;
    Unit _targetUnit;
    public void CheckMoveAndAttack()
    {
        //각자 알아서 찾고 이동해
        ProvokeState _pState = (ProvokeState)_stateMgr.GetState(EnumInfo.State.Provoke);
        if (_pState._curProvokeStateInfo._resourceUnit != null)
        {
            if (_pState._curProvokeStateInfo._resourceUnit._stateMgr._isLiving)
            {
                ProcessMoveAndAttack(_pState._curProvokeStateInfo._resourceUnit);
                return;
            }                
        }

        if (_targetUnit != null) {
            if (!_targetUnit.gameObject.activeSelf)
                _targetUnit = UnitManager.Instance.SearchEnemyUnit(this);
        }
        else
        {
            _targetUnit = UnitManager.Instance.SearchEnemyUnit(this);
        }
        

        if (_targetUnit == null)
            return;
        ProcessMoveAndAttack(_targetUnit);
    }


    void ProcessMoveAndAttack(Unit _tUnit)
    {
        _animMgr.UpdateDirection(_tUnit._tr.position - _tr.position);
        if (_unitData.AttackDistance >= Vector2.Distance(_tr.position, _tUnit._tr.position)) // 공격 가능 상태
        {
            _ableToAttack = true;
            _needToMove = false;
            _behaviourMgr.DoBehaviour(_tUnit);
        }
        else
        {
            _ableToAttack = false;
            _needToMove = true;
            _moveMgr.MoveToUnit(_tUnit);
        }
    }


    public void RestorePosition()
    {
        _moveMgr.RestorePosition();
    }

    public void FinishBattle()
    {
        _stateMgr.FinishBattle();
        _animMgr.FinishBattle();
        _behaviourMgr.FinishBattle();
        _moveMgr.FinishBattle();

        _tr.position = _tile.transform.position;
        gameObject.SetActive(true);
    }




    public void Die()
    {
        _stateMgr.Die();
        _animMgr.Die();
        _behaviourMgr.Die();
        _moveMgr.Die();

        _needToMove = false;
        _ableToAttack = false;

        gameObject.SetActive(false);
        UnitManager.Instance.CheckBattleResult();
    }





    public void CheckAttackOrMove(Action _pCallback)
    {
        _unitMgrCallback = _pCallback;

        if (CheckAbleToAttack())//싸울 수 있으면
        {
            _needToMove = false;
            _unitMgrCallback.Invoke();
        }
        else
        {
            _needToMove = true;
            TryToMove();
        }

    }

    public delegate void PathCallBack(AStarPathTile a);
    Action _unitMgrCallback;
    public void TryToMove()
    {
        _moveMgr.GetNextTile(Function);
    }

    public void Function(AStarPathTile _aPTile)
    {

        if (!AstarPath.active.isScanning)
            AstarPath.active.Scan();
        _unitMgrCallback.Invoke();
    }

    List<AStarPathTile> _list = new List<AStarPathTile>();
    bool CheckAbleToAttack()
    {
        //실제 거리가 가까우면
        _list.Clear();

        //AStarPathTile _tile = GetTile((int)_aStartTile._vec2.x + 1, (int)_aStartTile._vec2.y);
        //if(_tile!= null)
        //    _list.Add(_tile);
        //_tile = GetTile((int)_aStartTile._vec2.x - 1, (int)_aStartTile._vec2.y);
        //if (_tile != null)
        //    _list.Add(_tile);
        // _tile = GetTile((int)_aStartTile._vec2.x, (int)_aStartTile._vec2.y + 1);
        //if (_tile != null)
        //    _list.Add(_tile);
        // _tile = GetTile((int)_aStartTile._vec2.x, (int)_aStartTile._vec2.y - 1);
        //if (_tile != null)
        //    _list.Add(_tile);

        //for(int i =0;i< _list.Count; i++)
        //{
        //    if (_list[i]._ownUnit == null)
        //        continue;

        //    if (_list[i]._ownUnit._teamType != _teamType)
        //        return true;
        //}

        return false;
    }
    AStarPathTile _nextTile;
    public void MoveToTile()
    {
        //if(_aStartTile != null)
        //    StartCoroutine(ProcessMoving(_nextTile._tile));
    }

    IEnumerator ProcessMoving(Tile _t)
    {
        if (!_needToMove)
            yield break;

        while (true)
        {
            _tr.position = Vector2.MoveTowards(_tr.position, _nextTile.transform.position, Time.deltaTime * 60);
            yield return null;
        }
    }




    public AStarPathTile GetTile(int _xIdx, int _yIdx)
    {
        int _index = _xIdx + _yIdx * 5;
        if (_index >= UnitMoveManager.Instance._aStarPathTiles.Length || _index < 0)
            return null;
        else
            return UnitMoveManager.Instance._aStarPathTiles[_index];
    }

}


