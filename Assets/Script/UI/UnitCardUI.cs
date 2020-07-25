using UnityEngine;
using UnityEngine.UI;

public class UnitCardUI : MonoBehaviour
{
    [SerializeField] private Lobby.LobbyUnitInfoUI _lobbyUnitInfoUI;

    private DeckData _deckData;

    public event System.Action CreateEvent;

    private EnumInfo.TeamType _spawnTeam;

    public void SetUnitData(DeckData _deckDa,EnumInfo.TeamType _team)
    {
        _spawnTeam = _team;

        _deckData = _deckDa;

        _lobbyUnitInfoUI.SetUI(_deckDa._unit);
    }

    public void CreateUnit()
    {
        TileManager._Instance.CreateUnit(_deckData._unit.UnitIdx, _spawnTeam);
        _deckData.isUsed = true;
        CreateEvent?.Invoke();
    }
}
