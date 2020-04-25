using UnityEngine;
using System.Collections.Generic;

public class PlayerDrawManager : MonoBehaviour
{
    [SerializeField] private UnitCardUI[] _unitCardUIs;

    private DeckData[] _canChoiceDecks=new DeckData[3];

    public void SetPlayerDraw(DeckData[] _playerDeck)
    {
        _canChoiceDecks = GetRandomDeck(_playerDeck);

        for(int i=0;i< _unitCardUIs.Length;i++)
        {
            _unitCardUIs[i].SetUnitData(_canChoiceDecks[i]);
            _unitCardUIs[i].CreateEvent += SelectDeckCall;
        }
    }

    public void SelectDeckCall()
    {
        for (int i = 0; i < _unitCardUIs.Length; i++)
        {
            _unitCardUIs[i].CreateEvent -= SelectDeckCall;
        }
    }

    public DeckData[] GetRandomDeck(DeckData[] _playerDeck)
    {
        List<DeckData> _newDeckList = new List<DeckData>();

        DeckData _randomData;

        for(int i=0;i<9999;i++)
        {
            if (_newDeckList.Count == 3)
                break;

            _randomData=_playerDeck[UnityEngine.Random.Range(0, _playerDeck.Length)];

            if (_randomData.isUsed)
                continue;

            _newDeckList.Add(_randomData);
        }

        return _newDeckList.ToArray();
    }
}
