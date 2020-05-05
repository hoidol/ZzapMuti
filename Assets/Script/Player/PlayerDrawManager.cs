using UnityEngine;
using System.Collections.Generic;

public class PlayerDrawManager : MonoBehaviour
{
    [SerializeField] private UnitCardUI[] _unitCardUIs;
    [SerializeField] private GameObject _reDrawButton;

    private DeckData[] _nowPlayerDeck;
    private DeckData[] _canChoiceDecks=new DeckData[3];
    private EnumInfo.TeamType _nowTeam;

    private System.Action _drawFunc;

    public void SetPlayerDraw(DeckData[] _playerDeck,EnumInfo.TeamType _drawTeam,System.Action _drawCall)
    {
        _nowPlayerDeck = _playerDeck;

        _nowTeam = _drawTeam;

        _canChoiceDecks = GetRandomDeck(_playerDeck);

        _reDrawButton.gameObject.SetActive(true);
        for (int i=0;i< _unitCardUIs.Length;i++)
        {
            _unitCardUIs[i].gameObject.SetActive(true);
            _unitCardUIs[i].SetUnitData(_canChoiceDecks[i], _drawTeam);
            _unitCardUIs[i].CreateEvent += SelectDeckCall;
        }

        _drawFunc = _drawCall;
    }

    public void ReDraw()
    {
        _canChoiceDecks = GetRandomDeck(_nowPlayerDeck);

        for (int i = 0; i < _unitCardUIs.Length; i++)
        {
            _unitCardUIs[i].SetUnitData(_canChoiceDecks[i], _nowTeam);
        }
    }

    public void SelectDeckCall()
    {
        for (int i = 0; i < _unitCardUIs.Length; i++)
        {
            _unitCardUIs[i].CreateEvent -= SelectDeckCall;
            _unitCardUIs[i].gameObject.SetActive(false);
        }

        _reDrawButton.gameObject.SetActive(false);

        _drawFunc();
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
