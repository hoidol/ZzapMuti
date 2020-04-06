using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _tileSpriteRenderer;
    public SpriteRenderer _TileSpriteRenderer
    {
        get { return _tileSpriteRenderer; }
    }

    [SerializeField] private Vector2 _tilePosIndex=new Vector2();
    public Vector2 _TilePosIndex
    {
        get { return _tilePosIndex; }
    }

    public void Initialize(Vector2 _tilePosIdx, float _distance)
    {
        _tilePosIndex = _tilePosIdx;

        transform.localPosition = _tilePosIdx * _distance;
    }

    private Unit _unitIndex;
    public Unit _UnitIndex
    {
        get { return _unitIndex; }
    }

    private Unit _reservationUnit;
    public Unit _ReservationUnit
    {
        get { return _reservationUnit; }
    }

    public bool hasUnit = false;

    public void SetUnit(Unit _unit)
    {
        _unitIndex = _unit;

        _unit._tile = this;

        if (_unit == null)
            _tileSpriteRenderer.color = Color.white;
        else
            _tileSpriteRenderer.color = Color.black;
    }
    public void SetUnit(string _unitIdx, TeamType _teamTy)
    {
        UnitData _uniData= DataManager.Instance.GetUnitDataWithUnitIdx(_unitIdx);

        hasUnit = true;

        _unitIndex._tile = this;
        _unitIndex = UnitManager.Instance.CreateUnitWithUnitIdx(_unitIdx, _teamTy);
        _unitIndex.transform.position = this.transform.position;

        _tileSpriteRenderer.color = Color.black;
    }
}
