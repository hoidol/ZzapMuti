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

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    public void InitUnitMgr()
    {
        _curUnitsOnTile.Clear();
    }


    public void StartBattle()
    {
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

        return _unit;         
    }

    public Unit CombineUnit(Unit _tUnit, Unit _mUnit) // 유닛 병합
    {
        

    }


    public void RemoveUnit(Unit _u) // 캐릭터 병합 시 
    {
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


    UnitData GetNextLvUnit(UnitData _curUData)
    {
        int _nextLv = _curUData.ReinforceLv+1;
        for(int i =0; i < DataManager.Instance._unitDataContainer.UnitData.Length; i++)
        {
            if (DataManager.Instance._unitDataContainer.UnitData[i].UnitName.Equals(_curUData.UnitName))
            {
                if (DataManager.Instance._unitDataContainer.UnitData[i].ReinforceLv == _nextLv)
                    return DataManager.Instance._unitDataContainer.UnitData[i];
            }
        }

        Debug.LogError("강화 시킬 유닛이 없습니다.");
        return null;
    }


}
