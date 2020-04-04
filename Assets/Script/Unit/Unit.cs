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

    public void InitUnit(TeamType _tType)
    {
        _tr = transform;
        _unitData = DataManager.Instance.GetUnitDataWithUnitIdx(_unitIdx);
        _teamType = _tType;

    }



    
}
