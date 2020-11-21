using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    public string unitName;
    public UnitData unitData;
    public Transform _tr;
    public EnumInfo.TeamType _teamType;
    public Tile _tile;

    public UnitRealData unitRealData;

    [HideInInspector] public StateManager stateMgr;
    [HideInInspector] public AnimManager animMgr;
    [HideInInspector] public BehaviourManager behaviourMgr;
    [HideInInspector] public MoveManager moveMgr;

    // 초기 스텟 유닛 테이터
    // 실제 사용 용 유닛 데이터
    public ProvokeState provokeState;
    public Unit targetUnit;

    public bool ableToAttack;
    public bool needToMove;

    public List<CharacterInfoData> _characterInfoDataList= new List<CharacterInfoData>();
    public virtual void InitUnit()
    {
        _tr = transform;

        stateMgr = GetComponentInChildren<StateManager>();
        animMgr = GetComponentInChildren<AnimManager>();
        behaviourMgr = GetComponentInChildren<BehaviourManager>();
        moveMgr = GetComponentInChildren<MoveManager>();

        stateMgr.InitStateMgr(this);
        animMgr.InitAnimMgr(this);
        behaviourMgr.InitBehaviourMgr(this);
        moveMgr.InitMoveMgr(this);

        provokeState = (ProvokeState)stateMgr.GetState(EnumInfo.State.Provoke);
    }

    public virtual void SetUserData(EnumInfo.TeamType _tType, UnitData _uData)
    {
        _teamType = _tType;
        unitData = _uData;
        unitRealData.InitUnitRealData(this);
        animMgr.SetUserData(unitData, _teamType);
        behaviourMgr.SetUserData(unitData, _teamType);

        if (!unitData.Character.Equals("0"))
        {
            string[] _chars = unitData.Character.Split('/');
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

        stateMgr.StartBattle();
        animMgr.StartBattle();
        behaviourMgr.StartBattle();
        moveMgr.StartBattle();


        _tr.position = _tile.transform.position;

    }


    public virtual void TakeImpactData(UnitImpactData _uImpactData)
    {
        stateMgr.TakeDamage(_uImpactData.damage);
        stateMgr.ChangeState(_uImpactData.changeState);
    }

    public virtual void SetPosition()
    {
        moveMgr.SetPosition();
    }
    
    public virtual void StartBehaviour()
    {
        behaviourMgr.StartBehaviour();
        StartCoroutine(ProcessSearch());
    }
      
    public virtual void RestorePosition()
    {
        moveMgr.RestorePosition();
    }

    public virtual void FinishBattle()
    {
        stateMgr.FinishBattle();
        animMgr.FinishBattle();
        behaviourMgr.FinishBattle();
        moveMgr.FinishBattle();

        _tr.position = _tile.transform.position;
        gameObject.SetActive(true);

        unitRealData.InitUnitRealData(this);
    }

    public virtual void Die()
    {
        stateMgr.Die();
        animMgr.Die();
        behaviourMgr.Die();
        moveMgr.Die();

        needToMove = false;
        ableToAttack = false;

        gameObject.SetActive(false);
        UnitManager.Instance.CheckBattleResult();
    }

    public virtual IEnumerator ProcessSearch()
    {
        yield return null;
    }

    public virtual IEnumerator ProcessCoolTime()
    {
        yield return null;
    }
    public bool CheckAbleToUseSkill()
    {
        if (stateMgr._curMana >= unitRealData.MaxMana){
            return true;
        }
        return false;
    }
}


