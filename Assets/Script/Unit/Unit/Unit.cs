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
    public ProvokeState provokeState;
    public Unit _targetUnit;
    public bool _ableToAttack;
    public bool _needToMove;

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

    public virtual IEnumerator ProcessBehaviour()
    {
        yield return null;
    }

    public bool CheckAbleToUseSkill()
    {
        if (_stateMgr._curMana >= unitRealData.MaxMana){
            return true;
        }
        return false;
    }
}


