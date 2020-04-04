using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static DataManager _instance;
    public UnitDataContainer _unitDataContainer;

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
    }

    public UnitData GetUnitDataWithUnitName(string _uName)
    {
        for(int i = 0; i < _unitDataContainer.UnitData.Length; i++)
        {
            if (_unitDataContainer.UnitData[i].UnitName.Equals(_uName))
            {
                return _unitDataContainer.UnitData[i];
            }
        }
        return null;
    }

}

