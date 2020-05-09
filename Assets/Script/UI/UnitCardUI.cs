using UnityEngine;
using UnityEngine.UI;

public class UnitCardUI : MonoBehaviour
{
    [SerializeField] private Text _unitIdxText;
    [SerializeField] private Text _maxReinforceText;

    private DeckData _deckData;

    public event System.Action CreateEvent;

    private EnumInfo.TeamType _spawnTeam;

    public void SetUnitData(DeckData _deckDa,EnumInfo.TeamType _team)
    {
        _spawnTeam = _team;

        _deckData = _deckDa;
        
        _unitIdxText.text = _deckDa._unit.UnitName;
        _maxReinforceText.text =string.Format("최대 강화 [{0}]", _deckDa._unit.MaxReinforce);
    }

    public void CreateUnit()
    {
        TileManager._Instance.CreateUnit(_deckData._unit.UnitIdx, _spawnTeam);
        _deckData.isUsed = true;
        CreateEvent?.Invoke();
    }
}
