using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    public string unitName;
    public UnitData _unitData;
    public Transform _tr;
    public EnumInfo.TeamType _teamType;
    public Tile _tile;

    public UnitRealData unitRealData;

    [HideInInspector] public StateManager _stateMgr;
    [HideInInspector] public AnimManager _animMgr;
    [HideInInspector] public BehaviourManager _behaviourMgr;
    [HideInInspector] public MoveManager _moveMgr;

    // 초기 스텟 유닛 테이터
    // 실제 사용 용 유닛 데이터
    ProvokeState provokeState;


    public List<CharacterInfoData> _characterInfoDataList= new List<CharacterInfoData>();
    public virtual void InitUnit()
    {
        _tr = transform;

        _stateMgr = GetComponentInChildren<StateManager>();
        _animMgr = GetComponentInChildren<AnimManager>();
        _behaviourMgr = GetComponentInChildren<BehaviourManager>();
        _moveMgr = GetComponentInChildren<MoveManager>();

        _stateMgr.InitStateMgr(this);
        _animMgr.InitAnimMgr(this);
        _behaviourMgr.InitBehaviourMgr(this);
        _moveMgr.InitMoveMgr(this);

        provokeState = (ProvokeState)_stateMgr.GetState(EnumInfo.State.Provoke);
    }

    public virtual void SetUserData(EnumInfo.TeamType _tType, UnitData _uData)
    {
        _teamType = _tType;
        _unitData = _uData;
        unitRealData.InitUnitRealData(this);
        _animMgr.SetUserData(_unitData, _teamType);
        _behaviourMgr.SetUserData(_unitData, _teamType);

        if (!_unitData.Character.Equals("0"))
        {
            string[] _chars = _unitData.Character.Split('/');
            for (int i = 0; i < _chars.Length; i++)
                _characterInfoDataList.Add(DataManager.Instance.GetCharacterInfoDataWithCharacter(_chars[i]));
        }

    }

    public virtual void SetTile(Tile _t)
    {
        _tile = _t;
        _tr.position = _tile.transform.position;
    }

    public virtual void StartBattle()
    {      
        unitRealData.StartBattle();

        _stateMgr.StartBattle();
        _animMgr.StartBattle();
        _behaviourMgr.StartBattle();
        _moveMgr.StartBattle();


        _tr.position = _tile.transform.position;

    }


    public virtual void TakeImpactData(UnitImpactData _uImpactData)
    {
        _stateMgr.TakeDamage(_uImpactData.damage);
        _stateMgr.ChangeState(_uImpactData.changeState);
    }

    public virtual void SetPosition()
    {
        _moveMgr.SetPosition();
    }
    
    public virtual void StartBehaviour()
    {
        _behaviourMgr.StartBehaviour();
    }

  


    public virtual void RestorePosition()
    {
        _moveMgr.RestorePosition();
    }

    public virtual void FinishBattle()
    {
        _stateMgr.FinishBattle();
        _animMgr.FinishBattle();
        _behaviourMgr.FinishBattle();
        _moveMgr.FinishBattle();

        _tr.position = _tile.transform.position;
        gameObject.SetActive(true);

        unitRealData.InitUnitRealData(this);
    }

    public virtual void Die()
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


