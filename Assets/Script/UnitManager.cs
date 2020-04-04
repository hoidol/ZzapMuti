using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public Unit[] _units; //생성용 Prefab

    List<Unit> _poolingUnits = new List<Unit>();

    public List<Unit> _curUnitOnTile = new List<Unit>(); //타일에 있는 유닛들

    public void InitUnitMgr()
    {
        _curUnitOnTile.Clear();
    }


    public Unit CreateUnitWithUnitIdx(string _uIdx)
    {
        //        ....
        Unit _unit = BringAbleToUseUnit(_uIdx);
        _unit.InitUnit();
        return _unit;         
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
                return Instantiate(_units[i]);
            }
        }

        Debug.LogError("해당 유닛 인덱스를 찾을 수 없다.");
        return null;
    }


}
