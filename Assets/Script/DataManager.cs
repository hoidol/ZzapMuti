using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static DataManager _instance;
    public UnitDataContainer _unitDataContainer;
    public EntityDataContainer _entityDataContainer;
    public SynergyDataContainer _synergyDataContainer;
    public CharacterInfoDataContainer _characterInfoDataContainer;

    public static DataManager Instance
    {
        get
        {
            return _instance;
        }        
    }

    
    void Awake()
    {
        if(_instance == null){       
            _instance = this;
        }

        string _unitDataJStr = Resources.Load<TextAsset>("Json/UnitData").ToString();
        _unitDataContainer = JsonUtility.FromJson<UnitDataContainer>(_unitDataJStr);


        string _eDataJStr = Resources.Load<TextAsset>("Json/EntityData").ToString();
        _entityDataContainer = JsonUtility.FromJson<EntityDataContainer>(_eDataJStr);

        string _sDataJStr = Resources.Load<TextAsset>("Json/SynergyData").ToString();
        _synergyDataContainer = JsonUtility.FromJson<SynergyDataContainer>(_sDataJStr);

        string _sIDataJStr = Resources.Load<TextAsset>("Json/CharacterInfoData").ToString();
        _characterInfoDataContainer = JsonUtility.FromJson<CharacterInfoDataContainer>(_sIDataJStr);
    }

    public UnitData GetUnitDataWithUnitIdx(string _uIdx)
    {
        for (int i = 0; i < _unitDataContainer.UnitData.Length; i++)
        {
            if (_unitDataContainer.UnitData[i].UnitIdx.Equals(_uIdx))
            {
                return _unitDataContainer.UnitData[i];
            }
        }
        return null;
    }
    public UnitData GetUnitDataWithIdx(int _idx)
    {
        for (int i = 0; i < _unitDataContainer.UnitData.Length; i++)
        {
            if (_unitDataContainer.UnitData[i].Idx.Equals(_idx))
            {
                return _unitDataContainer.UnitData[i];
            }
        }
        return null;
    }

  /*  public UnitData GetUnitDataWithUnitName(string _uName)
    {
        for (int i = 0; i < _unitDataContainer.UnitData.Length; i++)
        {
            if (_unitDataContainer.UnitData[i].UnitName.Equals(_uName))
            {
                return _unitDataContainer.UnitData[i];
            }
        }
        return null;
    }*/

    public UnitData GetUnitDataWithUnitName(string _uName,int _lv)
    {
        for (int i = 0; i < _unitDataContainer.UnitData.Length; i++)
        {
            if (_unitDataContainer.UnitData[i].UnitName.Equals(_uName) && _unitDataContainer.UnitData[i].ReinforceLv == _lv)
            {
                return _unitDataContainer.UnitData[i];
            }
        }
        return null;
    }

    public EntityData GetEntityDataWithIdx(string _idx)
    {
        for(int i = 0; i < _entityDataContainer.EntityData.Length; i++)
        {
            if (_entityDataContainer.EntityData[i].EntityIdx.Equals(_idx))
            {
                return _entityDataContainer.EntityData[i];
            }
        }
        return null;
    }

    public SynergyData GetSynergyDataWithCharacterAndNum(string _c,int _n)
    {
        for(int i =0;i< _synergyDataContainer.SynergyData.Length; i++)
        {
            if(_c.Equals(_synergyDataContainer.SynergyData[i].Character) && _synergyDataContainer.SynergyData[i].NumberOfUnit == _n)
            {
                return _synergyDataContainer.SynergyData[i];
            }
        }
        return null;
    }

    public CharacterInfoData GetCharacterInfoDataWithCharacter(string _c)
    {
        for(int i = 0; i < _characterInfoDataContainer.CharacterInfoData.Length; i++)
        {
            if (_characterInfoDataContainer.CharacterInfoData[i].Character.Equals(_c))
            {
                return _characterInfoDataContainer.CharacterInfoData[i];
            }
        }
        return null;
    }


    public List<SynergyData> GetSynergyDataList(string _s)
    {
        List<SynergyData> _list = new List<SynergyData>();
        for (int i = 0; i < _synergyDataContainer.SynergyData.Length; i++)
        {
            if (_s.Equals(_synergyDataContainer.SynergyData[i].Character))
            {
                _list.Add(_synergyDataContainer.SynergyData[i]);
            }
        }
        return _list;
    }
}

