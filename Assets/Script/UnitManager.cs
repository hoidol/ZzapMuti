using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private static UnitManager _instance;

    public static UnitManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public Unit[] _units; //생성용 Prefab

    List<Unit> _poolingUnits = new List<Unit>();
    public List<Unit> _curUnitsOnTile = new List<Unit>(); //타일에 있는 유닛들
    public List<Unit> _curPlayerUnitsOnTile = new List<Unit>();
    public List<Unit> _curOppositeUnitsOnTile = new List<Unit>();

    public List<Unit> _curAliveUnitsOnTile = new List<Unit>();
    public List<Unit> _curAlivePlayerUnitsOnTile = new List<Unit>();
    public List<Unit> _curAliveOppositeUnitsOnTile = new List<Unit>();

    EnumInfo.TeamType _curTurnTeamType;

    public bool _playingBattle;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;

    }
    public void InitUnitMgr()
    {
        _curUnitsOnTile.Clear();
    }
    public void StartGame()
    {
        Debug.Log("StartGame()");
        _curTurnTeamType = Random.Range(0, 2) == 0 ? EnumInfo.TeamType.Player : EnumInfo.TeamType.Opposite;
    }

    public void StartBattle()
    {
        _playingBattle = true;
        Debug.Log("StartBattle()");
        // 어쌔신이 어느 타일로 이동되야되는지 

        // 레드팀 처리 따로, 블루팀 처리 따로
        CheckAbleUnit();
        for (int i = 0; i < _curUnitsOnTile.Count; i++)
        {
            if (!_curUnitsOnTile[i].gameObject.activeSelf)
                continue;
            _curUnitsOnTile[i].StartBattle();
        }


        PlayerManager.Instance.StartBattle();
        StartCoroutine(ProcessUnit());
    }


    IEnumerator ProcessUnit()
    {
        yield return new WaitForSeconds(0.75f);

        for (int i = 0; i < _curUnitsOnTile.Count; i++)
        {
            if (!_curUnitsOnTile[i].gameObject.activeSelf)
                continue;
            _curUnitsOnTile[i].SetPosition();
        }

        yield return new WaitForSeconds(1f);
        while (true)
        {
            for (int i = 0; i < _curUnitsOnTile.Count; i++)
            {
                if (!_curUnitsOnTile[i].gameObject.activeSelf)
                    continue;
                _curUnitsOnTile[i].CheckMoveAndAttack();
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    //이건 근거리고

    //원거리 유닛은 이걸로 가져올 필요가 없지

    //타겟 적 유닛 
    public Unit SearchEnemyUnit(Unit _unit)
    {
        // 탐색
        float _minDis = float.MaxValue;
        Unit _targetUnit = null;
        for (int i = 0; i < _curUnitsOnTile.Count; i++)
        {
            if (!_curUnitsOnTile[i].gameObject.activeSelf)
                continue;

            if (_unit._teamType == _curUnitsOnTile[i]._teamType)
                continue;
            float _tempDis = Vector2.SqrMagnitude(_unit._tr.position - _curUnitsOnTile[i]._tr.position);
            if (_minDis > _tempDis)
            {
                _minDis = _tempDis;
                _targetUnit = _curUnitsOnTile[i];
            }
        }
        return _targetUnit;
    }

    public void FinishBattle()
    {
        StopAllCoroutines();
        for (int i = 0; i < _curUnitsOnTile.Count; i++)
            _curUnitsOnTile[i].FinishBattle();
        
        _playingBattle = false;
    }


    void CheckAbleUnit()
    {
        _curAliveUnitsOnTile.Clear();
        _curAlivePlayerUnitsOnTile.Clear();
        _curAliveOppositeUnitsOnTile.Clear();

        for (int i = 0; i < _curUnitsOnTile.Count; i++)
        {
            if (_curUnitsOnTile[i]._stateMgr._isLiving)
            {
                _curAliveUnitsOnTile.Add(_curUnitsOnTile[i]);

                switch (_curUnitsOnTile[i]._teamType)
                {
                    case EnumInfo.TeamType.Player:
                        _curAlivePlayerUnitsOnTile.Add(_curUnitsOnTile[i]);
                        break;

                    case EnumInfo.TeamType.Opposite:
                        _curAliveOppositeUnitsOnTile.Add(_curUnitsOnTile[i]);
                        break;
                }
            }
        }
    }
    public void CheckBattleResult() //유닛이 죽을 때마다 호출됨
    {
        //블루 팀, 레드팀 전멸 확인

        //... 작업
        //라이프 깎을 때 고려될 수 있는 부분 : 유닛의 개수 + 유닛의 강화된 수 


        CheckAbleUnit();


        if (_curAlivePlayerUnitsOnTile.Count <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(ProcessCheckDraw(EnumInfo.TeamType.Opposite));
        }
        else if(_curAliveOppositeUnitsOnTile.Count <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(ProcessCheckDraw(EnumInfo.TeamType.Player));
        }

    }




    IEnumerator ProcessCheckDraw(EnumInfo.TeamType _winTeam)
    {
        for (int i = 0; i < _curUnitsOnTile.Count; i++)
            _curUnitsOnTile[i].RestorePosition();

        yield return new WaitForSeconds(0.5f);

        _curAliveUnitsOnTile.Clear();
        _curAlivePlayerUnitsOnTile.Clear();
        _curAliveOppositeUnitsOnTile.Clear();

        for (int i = 0; i < _curUnitsOnTile.Count; i++)
        {
            if (_curUnitsOnTile[i]._stateMgr._isLiving)
            {
                _curAliveUnitsOnTile.Add(_curUnitsOnTile[i]);

                switch (_curUnitsOnTile[i]._teamType)
                {
                    case EnumInfo.TeamType.Player:
                        _curAlivePlayerUnitsOnTile.Add(_curUnitsOnTile[i]);
                        break;

                    case EnumInfo.TeamType.Opposite:
                        _curAliveOppositeUnitsOnTile.Add(_curUnitsOnTile[i]);
                        break;
                }
            }
        }

        switch (_winTeam)
        {
            case EnumInfo.TeamType.Player:
                if (_curAlivePlayerUnitsOnTile.Count > 0)
                { 
                    StartCoroutine(ProcessResult(EnumInfo.TeamType.Player, _curAlivePlayerUnitsOnTile.Count));
                }
                else
                {
                    StartCoroutine(ProcessResult(EnumInfo.TeamType.Draw,0));
                }
                yield break;

            case EnumInfo.TeamType.Opposite:
                if (_curAliveOppositeUnitsOnTile.Count > 0) //레드 승
                {
                    StartCoroutine(ProcessResult(EnumInfo.TeamType.Opposite, _curAliveOppositeUnitsOnTile.Count));
                }
                else
                {
                    StartCoroutine(ProcessResult(EnumInfo.TeamType.Draw, 0));
                }
                yield break;
        }
    }






    IEnumerator ProcessResult(EnumInfo.TeamType _winTeam, int _loseLife)
    {
        yield return new WaitForSeconds(2f);
        GameProgress.Instance.EndBattle(_winTeam, _loseLife);
        PlayerManager.Instance.FinishBattle();
        FinishBattle();
    }

    

    public void TimeOver()
    {
        for(int i =0;i< _curUnitsOnTile.Count; i++)
        {
            if (!_curUnitsOnTile[i].gameObject.activeSelf)
                continue;
            _curUnitsOnTile[i]._stateMgr.Penalty();
        }
    }

    public Unit CreateUnitWithUnitIdx(string _uIdx, Tile _t, EnumInfo.TeamType _tType)
    {
        //        ....
        Unit _unit = BringAbleToUseUnit(_uIdx);
        _unit.InitUnit(_tType);
        _unit.SetTile(_t);

        _curUnitsOnTile.Add(_unit);

        switch (_tType)
        {
            case EnumInfo.TeamType.Player:
                _curPlayerUnitsOnTile.Add(_unit);
                break;
            case EnumInfo.TeamType.Opposite:
                _curOppositeUnitsOnTile.Add(_unit);
                break;
        }

        SynergyManager.Instance.CheckSynergy();

        return _unit;
    }


    public void UnitMoveToTile(Unit _u, Tile _t)
    {
        _u.SetTile(_t);        
    }


    public Unit CombineUnit(Unit _tUnit, Unit _mUnit,Tile _t) // 유닛 병합
    {
        int _nextLv = _tUnit._unitData.ReinforceLv + _mUnit._unitData.ReinforceLv;
        return CreateUnitWithUnitIdx(GetNextLvUnit(_tUnit._unitData.UnitName, _nextLv).UnitIdx, _t,_tUnit._teamType);
    }



    public void RemoveUnit(Unit _u) // 캐릭터 병합 시 
    {
        if (_u._teamType == EnumInfo.TeamType.Player)
            _curPlayerUnitsOnTile.Remove(_u);
        else
            _curOppositeUnitsOnTile.Remove(_u);

        _u.gameObject.SetActive(false);
        _curUnitsOnTile.Remove(_u);
    }



    Unit BringAbleToUseUnit(string _uIdx)
    {
        for(int i =0;i< _poolingUnits.Count; i++)
        {
            if (_poolingUnits[i].gameObject.activeSelf)
                continue;
            if (_poolingUnits[i]._unitIdx.Equals(_uIdx))
                return _poolingUnits[i];
        }
        return CreateUnit(_uIdx);
    }



    Unit CreateUnit(string _uIdx)
    {

        for(int i =0;i< _units.Length;i++)
        {
            if (_units[i]._unitIdx.Equals(_uIdx))
            {
                Unit _u = Instantiate(_units[i]);
                _poolingUnits.Add(_u);
                return _u;
            }
        }

        Debug.LogError("해당 유닛 인덱스를 찾을 수 없다.");
        return null;
    }


    UnitData GetNextLvUnit(string _uName,int _nextLv)
    {
        for(int i =0; i < DataManager.Instance._unitDataContainer.UnitData.Length; i++)
        {
            if (DataManager.Instance._unitDataContainer.UnitData[i].UnitName.Equals(_uName))
            {
                if (DataManager.Instance._unitDataContainer.UnitData[i].ReinforceLv == _nextLv)
                    return DataManager.Instance._unitDataContainer.UnitData[i];
            }
        }

        Debug.LogError("강화 시킬 유닛이 없습니다.");
        return null;
    }

   /* private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            //for (int i = 0; i < AstarPath.active.graphs.Length; i++)
            //{
            //    Debug.Log("AstarPath.active.graphs : " + AstarPath.active.graphs[i].graphIndex);
            //}

            //for (int i = 0; i < _curUnitsOnTile.Count; i++)
            //    _curUnitsOnTile[i].InitUnit(EnumInfo.TeamType.Red);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            //AstarPath.active.Scan();
            StartGame();
            StartBattle();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            AstarPath.active.Scan();
        }
    }*/

    public List<Unit> GetAliveUnitList(EnumInfo.TeamType _team)
    {
        if (_team.Equals(EnumInfo.TeamType.Player))
        {
            return _curAlivePlayerUnitsOnTile;
        }
        else
        {
            return _curAliveOppositeUnitsOnTile;
        }
    }

}
