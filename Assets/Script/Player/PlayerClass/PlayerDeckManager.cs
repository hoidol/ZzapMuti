using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeckData
{
    public UnitData _unit;
    public bool isUsed=false;
}

[System.Serializable]
public class PlayerDeckManager
{
    [SerializeField] private string[] _deckKind;

    [SerializeField] private DeckData[] _allDeck;
    public DeckData[] _Deck
    {
        get { return _allDeck; }
    }

    public void SetAllDeck()
    {
        UnitData[] _dataTemp = new UnitData[_deckKind.Length];

        int _allDeckCount=0;
        for(int i=0;i<_dataTemp.Length;i++)
        {
            _dataTemp[i] = DataManager.Instance.GetUnitDataWithUnitName(_deckKind[i]);

            _allDeckCount += _dataTemp[i].MaxReinforce;
        }
        _allDeck = new DeckData[_allDeckCount];

        int _choiceDeckIndex = 0;
        //덱 종류만큼 실행
        for(int i=0;i<_dataTemp.Length;i++)
        {
            //한 종류의 최대 강화개수만큼 실행
            for(int j=0;j<_dataTemp[i].MaxReinforce;j++)
            {
                _allDeck[_choiceDeckIndex] = new DeckData() { _unit=_dataTemp[i]};
                _choiceDeckIndex++;
            }
        }
    }
}
