using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCardUI : MonoBehaviour
{
    [SerializeField] private Text _unitIdxText;

    private UnitData _unitData;

    public void Start()
    {
        SetUnitData(DataManager.Instance._unitDataContainer.UnitData[9]);
    }

    public void SetUnitData(UnitData _unitDa)
    {
        _unitData = _unitDa;

        _unitIdxText.text = _unitData.UnitIdx;
    }

    public void CreateUnit()
    {
        TileManager._Instance.CreateUnit(_unitData.UnitIdx,PlayerTurnManager._Instance._CreateUnitPlayer);
    }
}
