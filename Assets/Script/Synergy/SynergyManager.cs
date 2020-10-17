using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyManager : MonoBehaviour
{
    public static SynergyManager Instance;

    public GameObject _synergyPanel;
    public Transform _synergyInfoPanelParent;
    public SynergyInfoPanel _synergyInfoPanel;
    public List<SynergyInfoPanel> _synergyInfoPanelList = new List<SynergyInfoPanel>();

    public Synergy[] _synergies;

    [SerializeField] List<CharacterCount> _playCharacterCountList = new List<CharacterCount>();
    [SerializeField] List<CharacterCount> _oppositeCharacterCountList = new List<CharacterCount>();
    List<string> _checkUnitNameList = new List<string>();
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        _synergyPanel.SetActive(false);
        InitSynergyMgr();
    }

    public void InitSynergyMgr()
    {
        _synergies = GetComponentsInChildren<Synergy>();
        for (int i = 0; i < _synergies.Length; i++)
            _synergies[i].InitSynergy();
    }


    public void CheckSynergy()
    {
        _playCharacterCountList.Clear();
        _oppositeCharacterCountList.Clear();
        _checkUnitNameList.Clear();
        for (int  i=0;i< UnitManager.Instance._curPlayerUnitsOnTile.Count; i++)
        {
            bool overlap = false;
            for(int j =0;j< _checkUnitNameList.Count; j++) //같은 유닛은 한번만 시너지 체크함
            {
                if (_checkUnitNameList[j].Equals(UnitManager.Instance._curPlayerUnitsOnTile[i]._unitData.UnitName))
                {
                    overlap = true;
                    break;
                }
            }
            if (overlap)
                continue;

            for (int j=0;j< UnitManager.Instance._curPlayerUnitsOnTile[i]._characterInfoDataList.Count; j++)
            {
                Debug.Log("UnitManager.Instance._curPlayerUnitsOnTile[i]._characterInfoDataList : " + UnitManager.Instance._curPlayerUnitsOnTile[i]._characterInfoDataList[j].Character);
                CharacterCount _charCount = GetPlayerCharacterCount(UnitManager.Instance._curPlayerUnitsOnTile[i]._characterInfoDataList[j].Character);
                _charCount.UnitList.Add(UnitManager.Instance._curPlayerUnitsOnTile[i]);
                _charCount.NumberOfUnit++;
            }
            _checkUnitNameList.Add(UnitManager.Instance._curPlayerUnitsOnTile[i]._unitData.UnitName);
        }

        _checkUnitNameList.Clear();
        for (int i = 0; i < UnitManager.Instance._curOppositeUnitsOnTile.Count; i++)
        {
            bool overlap = false;
            for (int j = 0; j < _checkUnitNameList.Count; j++)
            {
                if (_checkUnitNameList[j].Equals(UnitManager.Instance._curOppositeUnitsOnTile[i]._unitData.UnitName))
                {
                    overlap = true;
                    break;
                }
            }
            if (overlap)
                continue;

            for (int j = 0; j < UnitManager.Instance._curOppositeUnitsOnTile[i]._characterInfoDataList.Count; j++)
            {
                CharacterCount _charCount = GetOppositeCharacterCount(UnitManager.Instance._curOppositeUnitsOnTile[i]._characterInfoDataList[j].Character);
                _charCount.NumberOfUnit++;
            }
            _checkUnitNameList.Add(UnitManager.Instance._curOppositeUnitsOnTile[i]._unitData.UnitName);
        }

        ShowPlayerSynergy();
    }



    void ShowPlayerSynergy()
    {
        _synergyPanel.SetActive(false);
        for (int i = 0; i < _synergyInfoPanelList.Count; i++)
            _synergyInfoPanelList[i].gameObject.SetActive(false);

        for (int i = 0; i < _playCharacterCountList.Count; i++)
        {
            if (!_synergyPanel.activeSelf)
                _synergyPanel.SetActive(true);

            SynergyInfoPanel _s = GetSynergyInfoPanel();
            _s.gameObject.SetActive(true);
            _s.UpdateSynergyInfo(_playCharacterCountList[i]);
        }

        _synergyInfoPanelList.Sort(delegate (SynergyInfoPanel _a, SynergyInfoPanel _b)
        {
            if (_a._charCount.NumberOfUnit > _b._charCount.NumberOfUnit) return 1;
            if (_a._charCount.NumberOfUnit < _b._charCount.NumberOfUnit) return -1;
            return 0;
        });

        for (int i = 0; i < _synergyInfoPanelList.Count; i++)
            _synergyInfoPanelList[i]._rectTr.SetSiblingIndex(i);
    }

    void ShowOppositeSynergy()
    {
        _synergyPanel.SetActive(false);
        for (int i = 0; i < _synergyInfoPanelList.Count; i++)
        {
            _synergyInfoPanelList[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < _oppositeCharacterCountList.Count; i++)
        {
            if (!_synergyPanel.activeSelf)
                _synergyPanel.SetActive(true);

            SynergyInfoPanel _s = GetSynergyInfoPanel();
            _s.gameObject.SetActive(true);
            _s.UpdateSynergyInfo(_oppositeCharacterCountList[i]);
        }

        _synergyInfoPanelList.Sort(delegate (SynergyInfoPanel _a, SynergyInfoPanel _b)
        {
            if (_a._charCount.NumberOfUnit > _b._charCount.NumberOfUnit) return 1;
            if (_a._charCount.NumberOfUnit < _b._charCount.NumberOfUnit) return -1;
            return 0;
        });

        for (int i = 0; i < _synergyInfoPanelList.Count; i++)
            _synergyInfoPanelList[i]._rectTr.SetSiblingIndex(i);
    }

    CharacterCount GetPlayerCharacterCount(string _c)
    {
        for(int i =0;i< _playCharacterCountList.Count; i++)
        {
            if (_playCharacterCountList[i].CharacterInfoData.Character.Equals(_c))
            {
                return _playCharacterCountList[i];
            }
        }

        CharacterCount _charCount = new CharacterCount();
        _charCount.CharacterInfoData = DataManager.Instance.GetCharacterInfoDataWithCharacter(_c);
        _charCount.NumberOfUnit = 0;
        _playCharacterCountList.Add(_charCount);
        return _charCount;
    }

    CharacterCount GetOppositeCharacterCount(string _c)
    {
        for (int i = 0; i < _oppositeCharacterCountList.Count; i++)
        {
            if (_oppositeCharacterCountList[i].CharacterInfoData.Character.Equals(_c))
            {
                return _oppositeCharacterCountList[i];
            }
        }

        CharacterCount _charCount = new CharacterCount();
        _charCount.CharacterInfoData = DataManager.Instance.GetCharacterInfoDataWithCharacter(_c);
        _charCount.NumberOfUnit = 0;
        _oppositeCharacterCountList.Add(_charCount);
        return _charCount;
    }

    public void StartBattle()
    {
        //적용시켜야돼        
        Debug.Log("SynergyMgr - StartBattle()");
        for (int i = 0; i < _playCharacterCountList.Count; i++)
        {
            Synergy _s = GetSynergy(_playCharacterCountList[i].CharacterInfoData, _playCharacterCountList[i].NumberOfUnit);

            Debug.Log("SynergyMgr - StartBattle() 1");
            if (_s == null)
                continue;

            Debug.Log("SynergyMgr - StartBattle() 2");
            for (int j = 0; j < _playCharacterCountList[i].UnitList.Count; j++)
            {
                Debug.Log("SynergyMgr - 적용할 수 있음!!@!@");
                _s.ApplySynergy(_playCharacterCountList[i].UnitList[j]);
            }            
        }

        for(int i=0;i< _oppositeCharacterCountList.Count; i++)
        {
            Synergy _s = GetSynergy(_oppositeCharacterCountList[i].CharacterInfoData, _playCharacterCountList[i].NumberOfUnit);
            if (_s == null)
                continue;
            for (int j = 0; j < _oppositeCharacterCountList[i].UnitList.Count; j++)
            {
                Debug.Log("SynergyMgr - 적용할 수 있음!!@!@");
                _s.ApplySynergy(_oppositeCharacterCountList[i].UnitList[j]);
            }           
        }

    }



    public SynergyData GetSynergyData(string _cIdx,int _c)
    {
        List<SynergyData> _list = DataManager.Instance.GetSynergyDataList(_cIdx);
        int _curSynergyLv = 0;
        SynergyData _synergyData = null;
        for (int i =0;i< _list.Count; i++)
        {
            if (_list[i].Character.Equals(_cIdx))
            {
                if(_c >= _list[i].NumberOfUnit && _list[i].NumberOfUnit >= _curSynergyLv)
                {
                    _curSynergyLv = _list[i].NumberOfUnit;
                    _synergyData = _list[i];
                }
            }
        }
        return _synergyData;
    }

    SynergyInfoPanel GetSynergyInfoPanel()
    {
        for(int i=0;i< _synergyInfoPanelList.Count; i++)
        {
            if (_synergyInfoPanelList[i].gameObject.activeSelf)
                continue;
            return _synergyInfoPanelList[i];
        }

        SynergyInfoPanel _s = Instantiate(_synergyInfoPanel, _synergyInfoPanelParent);
        _s.InitSynergyInfo();
        _synergyInfoPanelList.Add(_s);
        return _s;
    }

    Synergy GetSynergy(CharacterInfoData _cInfo, int _uNum)
    {
        int _curTargetNum = -1;
        int _targetIdx = -1;
        Debug.Log("SynergyMgr GetSynergy " + _cInfo.Character + " _uNum : " +_uNum);
        for (int i=0;i< _synergies.Length; i++)
        {
            Debug.Log("SynergyMgr _synergies[i] " + _synergies[i]._synergyIdx);
            if (_synergies[i]._synergyIdx.Equals(_cInfo.Character))
            {
                if (_uNum >= _synergies[i]._synergyCount && _synergies[i]._synergyCount > _curTargetNum)
                {
                    Debug.Log("SynergyMgr GetSynergy if (_uNum >= _synergies[i]._synergyCount && _synergies[i]._synergyCount > _curTargetNum) ");
                    _curTargetNum = _uNum;
                    _targetIdx = i;
                }
            }
        }

        if (_targetIdx != -1)
            return _synergies[_targetIdx];

        return null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Time.timeScale = 5;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Time.timeScale = 1;
        }
    }
}

[System.Serializable]
public class CharacterCount
{
    public CharacterInfoData CharacterInfoData;
    public List<Unit> UnitList = new List<Unit>();
    public int NumberOfUnit;
}