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
            for(int j =0;j< _checkUnitNameList.Count; j++)
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

        CheckPlayerSynergy();
        CheckOppositeSynergy();
        ShowPlayerSynergy();
    }


    void CheckPlayerSynergy()
    {
        for (int i = 0; i < _playCharacterCountList.Count; i++)
            _playCharacterCountList[i].SynergyData = GetSynergyData(_playCharacterCountList[i].CharacterInfoData.Character, _playCharacterCountList[i].NumberOfUnit);
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

    void CheckOppositeSynergy()
    {
        
        for (int i = 0; i < _oppositeCharacterCountList.Count; i++)
            _oppositeCharacterCountList[i].SynergyData = GetSynergyData(_oppositeCharacterCountList[i].CharacterInfoData.Character, _oppositeCharacterCountList[i].NumberOfUnit);
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
        for(int i =0;i< _playCharacterCountList.Count; i++)
        {
            if (_playCharacterCountList[i].SynergyData == null)
                continue;

            for (int j = 0; j < UnitManager.Instance._curPlayerUnitsOnTile.Count; j++)
            {
                for(int k=0; k< UnitManager.Instance._curPlayerUnitsOnTile[j]._characterInfoDataList.Count; k++)
                {
                   if(UnitManager.Instance._curPlayerUnitsOnTile[j]._characterInfoDataList[k].Equals(_playCharacterCountList[i].SynergyData.Character))
                    {
                        Synergy _s = GetSynergy(_playCharacterCountList[i].SynergyData.Character, _playCharacterCountList[i].SynergyData.NumberOfUnit);
                        _s.ApplySynergy(UnitManager.Instance._curPlayerUnitsOnTile[j]);
                        continue;
                    }
                }
            }
        }

        for(int i=0;i< _oppositeCharacterCountList.Count; i++)
        {
             if (_oppositeCharacterCountList[i].SynergyData == null)
                continue;

            for (int j = 0; j < UnitManager.Instance._curPlayerUnitsOnTile.Count; j++)
            {
                for (int k = 0; k < UnitManager.Instance._curPlayerUnitsOnTile[j]._characterInfoDataList.Count; k++)
                {
                    if (UnitManager.Instance._curPlayerUnitsOnTile[j]._characterInfoDataList[k].Equals(_oppositeCharacterCountList[i].SynergyData.Character))
                    {
                        Synergy _s = GetSynergy(_oppositeCharacterCountList[i].SynergyData.Character, _oppositeCharacterCountList[i].SynergyData.NumberOfUnit);
                        _s.ApplySynergy(UnitManager.Instance._curPlayerUnitsOnTile[j]);
                        continue;
                    }
                }

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

    Synergy GetSynergy(string _sIdx, int _c)
    {
        for(int i=0;i< _synergies.Length; i++)
        {
            if(_synergies[i]._synergyIdx.Equals(_sIdx) && _synergies[i]._synergyCount.Equals(_c))
            {
                return _synergies[i];
            }
        }
        return null;
    }
}

[System.Serializable]
public class CharacterCount
{
    public CharacterInfoData CharacterInfoData;
    public SynergyData SynergyData;
    public int NumberOfUnit;
}