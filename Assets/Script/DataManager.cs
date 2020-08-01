using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static DataManager _instance;
    public UnitDataContainer _unitDataContainer;
    public EntityDataContainer _entityDataContainer;
   
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
    }

    public UnitData GetUnitDataWithUnitIdx(string _uIdx)
    {
        for(int i = 0; i < _unitDataContainer.UnitData.Length; i++)
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

    public UnitData GetUnitDataWithUnitName(string _uName)
    {
        for (int i = 0; i < _unitDataContainer.UnitData.Length; i++)
        {
            if (_unitDataContainer.UnitData[i].UnitName.Equals(_uName))
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
            if (_entityDataContainer.EntityData[i].Idx.Equals(_idx))
            {
                return _entityDataContainer.EntityData[i];
            }
        }
        return null;
    }
}

