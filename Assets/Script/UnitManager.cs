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
    public List<Unit> _curRedUnitsOnTile = new List<Unit>();
    public List<Unit> _curBlueUnitsOnTile = new List<Unit>();

    TeamType _curTurnTeamType;
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
        _curTurnTeamType = Random.Range(0, 2) == 0 ? TeamType.Red : TeamType.Blue;
    }

    public void StartBattle()
    {
        StartCoroutine(ProcessBattle());
    }
    IEnumerator ProcessBattle()
    {
        yield return new WaitForSeconds(1); // 
        
        while (true)
        {
            _pathCallBackCount = _curUnitsOnTile.Count;
            for (int i = 0; i < _curUnitsOnTile.Count; i++)
                _curUnitsOnTile[i]._aStartTile.TakeTile(null, false);

            for (int i = 0; i < _curUnitsOnTile.Count; i++)
            {
                if (_curUnitsOnTile[i]._teamType == _curTurnTeamType)
                    _curUnitsOnTile[i].CheckAttackOrMove(PathCallBack);
            }
            // _curTurnTeamType  차례바꾸기
            if (_curTurnTeamType == TeamType.Red)
                _curTurnTeamType = TeamType.Blue;
            else
                _curTurnTeamType = TeamType.Red;


            for (int i = 0; i < _curUnitsOnTile.Count; i++)
            {
                if (_curUnitsOnTile[i]._teamType == _curTurnTeamType)
                    _curUnitsOnTile[i].CheckAttackOrMove(PathCallBack);
            }

            yield return new WaitForSeconds(1f);
        }
    }
    // 살아있는 모든 유닛 카운트
    public int _pathCallBackCount;
    public void PathCallBack()
    {
        _pathCallBackCount--;
        if (_pathCallBackCount <= 0)
        {
            Debug.Log("PathCallBack() 호출");
            for (int i = 0; i < _curUnitsOnTile.Count; i++)
                _curUnitsOnTile[i].MoveToTile();
        }
    }

    //이건 근거리고
              
    //원거리 유닛은 이걸로 가져올 필요가 없지

    //타겟 적 유닛 
    Unit SearchEnemyUnit(Unit _unit)
    {
        // 탐색
        float _minDis = float.MaxValue;
        Unit _targetUnit = null;
        for(int i =0;i< _curUnitsOnTile.Count; i++)
        {
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
    }

    public Unit CreateUnitWithUnitIdx(string _uIdx,TeamType _tType)
    {
        //        ....
        Unit _unit = BringAbleToUseUnit(_uIdx);
        _unit.InitUnit(_tType);
        _curUnitsOnTile.Add(_unit);

        switch (_tType)
        {
            case TeamType.Red:
                _curRedUnitsOnTile.Add(_unit);
                break;
            case TeamType.Blue:
                _curBlueUnitsOnTile.Add(_unit);
                break;
        }
        return _unit;         
    }

    public Unit CombineUnit(Unit _tUnit, Unit _mUnit) // 유닛 병합
    {
        int _nextLv = _tUnit._unitData.ReinforceLv + _mUnit._unitData.ReinforceLv;
        return CreateUnitWithUnitIdx(GetNextLvUnit(_tUnit._unitData.UnitName, _nextLv).UnitIdx, _tUnit._teamType);
    }


    public void RemoveUnit(Unit _u) // 캐릭터 병합 시 
    {
        if (_u._teamType == TeamType.Red)
            _curRedUnitsOnTile.Remove(_u);
        else
            _curBlueUnitsOnTile.Remove(_u);

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            for (int i = 0; i < AstarPath.active.graphs.Length; i++)
            {
                Debug.Log("AstarPath.active.graphs : " + AstarPath.active.graphs[i].graphIndex);
            }

            for (int i = 0; i < _curUnitsOnTile.Count; i++)
                _curUnitsOnTile[i].InitUnit(TeamType.Red);
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
    }

}
