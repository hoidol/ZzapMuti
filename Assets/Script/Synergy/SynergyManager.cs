using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyManager : MonoBehaviour
{
    public static SynergyManager Instance;
   
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


    

    List<CharacterCount> _playCharacterCountList = new List<CharacterCount>();
    List<CharacterCount> _oppositeCharacterCountList = new List<CharacterCount>();
    List<string> _checkUnitNameList = new List<string>();
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
                CharacterCount _charCount = GetPlayerCharacterCount(UnitManager.Instance._curOppositeUnitsOnTile[i]._characterInfoDataList[j].Character);
                _charCount.NumberOfUnit++;
            }
            _checkUnitNameList.Add(UnitManager.Instance._curOppositeUnitsOnTile[i]._unitData.UnitName);
        }
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



    }


}


public class CharacterCount
{
    public CharacterInfoData CharacterInfoData;
    public int NumberOfUnit;
}