using UnityEngine;
using UnityEngine.UI;

public class UnitCardUI : MonoBehaviour
{
    [SerializeField] private Text _unitIdxText;

    private DeckData _deckData;

    public event System.Action CreateEvent;

    public void SetUnitData(DeckData _deckDa)
    {
        _deckData = _deckDa;
        
        _unitIdxText.text = _deckDa._unit.UnitName;
    }

    public void CreateUnit()
    {
        TileManager._Instance.CreateUnit(_deckData._unit.UnitIdx,PlayerTurnManager._Instance._CreateUnitPlayer);
        CreateEvent?.Invoke();
    }
}
