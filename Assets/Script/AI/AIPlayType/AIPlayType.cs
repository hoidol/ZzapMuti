using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class AIPlayType : MonoBehaviour
{
    [HideInInspector]public EnumInfo.AIPlayType _aiPlayType;

    [SerializeField] protected List<UnitData> _selectedUnitData = new List<UnitData>();

    public List<UnitFeatureInfo> _playerUnitFeatureInfo = new List<UnitFeatureInfo>();
    public List<UnitFeatureInfo> _aiUnitFeatureInfo = new List<UnitFeatureInfo>();
    public List<UnitFeatureInfo> _gapUnitFeatureInfo = new List<UnitFeatureInfo>();

    public virtual void InitAIPlayType()
    {
        for(int i = 1; i <= EnumInfo.UnitFeatureTypeCount; i++)
        {
            UnitFeatureInfo _pUFInfo = new UnitFeatureInfo();
            _pUFInfo.FeatureType = (EnumInfo.UnitFeatureType)i;
            _pUFInfo.Value = 0;
            _playerUnitFeatureInfo.Add(_pUFInfo);
            UnitFeatureInfo _aUFInfo = new UnitFeatureInfo();
            _aUFInfo.FeatureType = (EnumInfo.UnitFeatureType)i;
            _aUFInfo.Value = 0;
            _aiUnitFeatureInfo.Add(_aUFInfo);

            UnitFeatureInfo _gUFInfo = new UnitFeatureInfo();
            _gUFInfo.FeatureType = (EnumInfo.UnitFeatureType)i;
            _gUFInfo.Value = 0;
            _gapUnitFeatureInfo.Add(_gUFInfo);
        }
    }

    public virtual void StartTurn(int _r)
    {
        ShuffleUnitCard();
    }

    public void ShuffleUnitCard()
    {
        _selectedUnitData.Clear();
        int random1;
        int random2;

        UnitData tmp;
        for (int i = 0; i < AIManager.Instance._opponentUnitDataDeckList.Count; ++i)
        {
            random1 = Random.Range(0, AIManager.Instance._opponentUnitDataDeckList.Count);
            random2 = Random.Range(0, AIManager.Instance._opponentUnitDataDeckList.Count);

            tmp = AIManager.Instance._opponentUnitDataDeckList[random1];
            AIManager.Instance._opponentUnitDataDeckList[random1] = AIManager.Instance._opponentUnitDataDeckList[random2];
            AIManager.Instance._opponentUnitDataDeckList[random2] = tmp;
        }

        for(int i =0;i< AIManager.Instance._opponentUnitDataDeckList.Count; i++)
        {
            _selectedUnitData.Add(AIManager.Instance._opponentUnitDataDeckList[i]);
            if (_selectedUnitData.Count >= 3)
                break;
        }

       
    }

    [SerializeField] protected int gap_Feature_PhysicDamage;
    [SerializeField] protected int gap_Feature_MasicDamage;
    [SerializeField] protected int gap_Feature_PhysicDefence;
    [SerializeField] protected int gap_Feature_MagicResistance;
    [SerializeField] protected int gap_Feature_CC;
    [SerializeField] protected int gap_Feature_Buff;
    [SerializeField] protected int gap_Feature_Distance;
    [SerializeField] protected int gap_Feature_Range;


    public void AnalyzeSituation()
    {        
        InitFeatureInfo();
        for (int i = 0; i < UnitManager.Instance._curPlayerUnitsOnTile.Count; i++)
        {
            UpdatePlayFeature(EnumInfo.UnitFeatureType.Feature_PhysicDamage, UnitManager.Instance._curPlayerUnitsOnTile[i]._unitData.Feature_PhysicDamage);
            UpdatePlayFeature(EnumInfo.UnitFeatureType.Feature_MasicDamage, UnitManager.Instance._curPlayerUnitsOnTile[i]._unitData.Feature_MasicDamage);
            UpdatePlayFeature(EnumInfo.UnitFeatureType.Feature_PhysicDefence, UnitManager.Instance._curPlayerUnitsOnTile[i]._unitData.Feature_PhysicDefence);
            UpdatePlayFeature(EnumInfo.UnitFeatureType.Feature_MagicResistance, UnitManager.Instance._curPlayerUnitsOnTile[i]._unitData.Feature_MagicResistance);
            UpdatePlayFeature(EnumInfo.UnitFeatureType.Feature_CC, UnitManager.Instance._curPlayerUnitsOnTile[i]._unitData.Feature_CC);
            UpdatePlayFeature(EnumInfo.UnitFeatureType.Feature_Buff, UnitManager.Instance._curPlayerUnitsOnTile[i]._unitData.Feature_Buff);
            UpdatePlayFeature(EnumInfo.UnitFeatureType.Feature_Distance, UnitManager.Instance._curPlayerUnitsOnTile[i]._unitData.Feature_Distance);
            UpdatePlayFeature(EnumInfo.UnitFeatureType.Feature_Range, UnitManager.Instance._curPlayerUnitsOnTile[i]._unitData.Feature_Range);
        }
        for (int i = 0; i < UnitManager.Instance._curOppositeUnitsOnTile.Count; i++)
        {
            UpdateAIFeature(EnumInfo.UnitFeatureType.Feature_PhysicDamage, UnitManager.Instance._curOppositeUnitsOnTile[i]._unitData.Feature_PhysicDamage);
            UpdateAIFeature(EnumInfo.UnitFeatureType.Feature_MasicDamage, UnitManager.Instance._curOppositeUnitsOnTile[i]._unitData.Feature_MasicDamage);
            UpdateAIFeature(EnumInfo.UnitFeatureType.Feature_PhysicDefence, UnitManager.Instance._curOppositeUnitsOnTile[i]._unitData.Feature_PhysicDefence);
            UpdateAIFeature(EnumInfo.UnitFeatureType.Feature_MagicResistance, UnitManager.Instance._curOppositeUnitsOnTile[i]._unitData.Feature_MagicResistance);
            UpdateAIFeature(EnumInfo.UnitFeatureType.Feature_CC, UnitManager.Instance._curOppositeUnitsOnTile[i]._unitData.Feature_CC);
            UpdateAIFeature(EnumInfo.UnitFeatureType.Feature_Buff, UnitManager.Instance._curOppositeUnitsOnTile[i]._unitData.Feature_Buff);
            UpdateAIFeature(EnumInfo.UnitFeatureType.Feature_Distance, UnitManager.Instance._curOppositeUnitsOnTile[i]._unitData.Feature_Distance);
            UpdateAIFeature(EnumInfo.UnitFeatureType.Feature_Range, UnitManager.Instance._curOppositeUnitsOnTile[i]._unitData.Feature_Range);
        }
        UpdateGapFeature();
    }


    void UpdatePlayFeature(EnumInfo.UnitFeatureType _uFType, int _v)
    {
        for (int i = 0; i < _playerUnitFeatureInfo.Count; i++)
        {
            if (_playerUnitFeatureInfo[i].FeatureType.Equals(_uFType))
            {
                _playerUnitFeatureInfo[i].Value += _v;
            }

        }
    }

    void UpdateAIFeature(EnumInfo.UnitFeatureType _uFType, int _v)
    {
        for (int i = 0; i < _aiUnitFeatureInfo.Count; i++)
        {
            if (_aiUnitFeatureInfo[i].FeatureType.Equals(_uFType))
            {
                _aiUnitFeatureInfo[i].Value += _v;
            }

        }
    }

    void UpdateGapFeature()
    {
        for(int i =0;i< _gapUnitFeatureInfo.Count; i++)
        {
            _gapUnitFeatureInfo[i].Value = _aiUnitFeatureInfo[i].Value - _playerUnitFeatureInfo[i].Value;
        }
    }

    void InitFeatureInfo()
    {
        for (int i = 0; i < _playerUnitFeatureInfo.Count; i++)
            _playerUnitFeatureInfo[i].Value = 0;
        for (int i = 0; i < _aiUnitFeatureInfo.Count; i++)
            _aiUnitFeatureInfo[i].Value = 0; 
        for (int i = 0; i < _gapUnitFeatureInfo.Count; i++)
            _gapUnitFeatureInfo[i].Value = 0;
    }

    protected UnitData SearchBestUnit(EnumInfo.UnitFeatureType _fType)
    {
        int _bestIdx = -1;
        int _maxValue = int.MinValue;
        for(int i = 0; i < _selectedUnitData.Count; i++)
        {
           int _tempV = GetFeatureVale(_fType, _selectedUnitData[i]);
            if(_tempV > _maxValue)
            {
                _maxValue = _tempV;
                _bestIdx = i;
            }
        }
        return _selectedUnitData[_bestIdx];
    }

    int GetFeatureVale(EnumInfo.UnitFeatureType _fType, UnitData _uD)
    {
        switch (_fType)
        {
            case EnumInfo.UnitFeatureType.Feature_PhysicDamage:
                return _uD.Feature_PhysicDamage;
            case EnumInfo.UnitFeatureType.Feature_MasicDamage:
                return _uD.Feature_MasicDamage;
            case EnumInfo.UnitFeatureType.Feature_PhysicDefence:
                return _uD.Feature_PhysicDefence;
            case EnumInfo.UnitFeatureType.Feature_MagicResistance:
                return _uD.Feature_MagicResistance;
            case EnumInfo.UnitFeatureType.Feature_Buff:
                return _uD.Feature_Buff;
            case EnumInfo.UnitFeatureType.Feature_CC:
                return _uD.Feature_CC;
            case EnumInfo.UnitFeatureType.Feature_Distance:
                return _uD.Feature_Distance;
            case EnumInfo.UnitFeatureType.Feature_Range:
                return _uD.Feature_Range;
        }
        return 0;
    }
}


[System.Serializable]
public class UnitFeatureInfo
{
    public EnumInfo.UnitFeatureType FeatureType;
    public int Value;
}